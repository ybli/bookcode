from sklearn.neighbors import NearestNeighbors
import numpy as np
import pymysql
from sklearn.cluster import AffinityPropagation
import matplotlib.pyplot as plt


X=np.array([[114.439872,30.579892],[114.367576,30.524907],[114.418169,30.521423],
 [114.418169,30.521423],[114.417594,30.550786],[114.372176,30.568698],
 [114.34113,30.547303],[114.3762,30.544815],[114.348604,30.507485],
 [114.396897,30.50848],[114.396897,30.50848],[114.377925,30.518436],[114.435416,30.511965]]
)
sql1='''SELECT bikeid,distX,distY FROM {}'''
sql2='''SELECT bikeid,distX,distY FROM {}'''
sql3='''
        create table {}(
        bikeId varchar(255) not null,
        OX float(32) not null,
        OY float(32) not null,
        DX float(32) not null,
        DY float(32) not null,
        Origin varchar(100) not null,
        Destination varchar(100) not null)DEFAULT CHARSET=utf8;
'''
sql4='''
        insert into {}(bikeId,OX,OY,DX,DY,Origin,Destination)
        values(%s,%s,%s,%s,%s,%s,%s);
    '''
sql5='''SELECT bikeid,OX,OY,DX,DY,origin,Destination FROM {}'''
dataName=np.array(['guanggu72209','guanggu72210','guanggu72211','guanggu72212','guanggu72213','guanggu72215','guanggu72216']
)
class KNNFLOW:
    def __init__(self,Train_x,O_x,D_x):
        self.Train_x=Train_x
        self.Tsize=Train_x.shape[0]
        self.O_x=O_x
        self.D_x=D_x
        self.O_size=O_x.shape[0]
        self.D_size=D_x.shape[0]
        self.flag=0
    def Knn(self):
        nbrs=NearestNeighbors(n_neighbors=1,algorithm='ball_tree').fit(self.Train_x)
        distances,indices=nbrs.kneighbors(self.O_x[:,1:])
        O_r=np.zeros((self.O_size,4))
        O_r[:,0:3]=self.O_x[:,:]
        O_r[:,3]=indices[:,0]
        distances,indices=nbrs.kneighbors(self.D_x[:,1:])
        D_r=np.zeros((self.D_size,4))
        D_r[:,0:3]=self.D_x[:,:]
        D_r[:,3]=indices[:,0]
        #print(indices)
        return O_r,D_r

    def BikeFlow(self):
        O,D=self.Knn()
        F_CDT=np.zeros((self.Tsize,self.Tsize))
        B_CDT=np.zeros((self.O_size,7))
        i=0
        B_CDT[:,6]=100
        for item1 in O:
            Oid=item1[0]
            B_CDT[i,0]=Oid
            B_CDT[i,1:3]=item1[1:3]
            B_CDT[i,5]=item1[3]
            #print(item1[3])
            for item2 in D:
                if Oid==item2[0]:
                    F_CDT[int(item1[3]),int(item2[3])]=F_CDT[int(item1[3]),int(item2[3])]+1
                    B_CDT[i,3:5]=item2[1:3]
                    B_CDT[i,6]=item2[3]
                    break
            i=i+1
        self.B_CDT=B_CDT
        self.F_CDT=F_CDT
        return F_CDT,B_CDT


    def SaveData(self,sqlName):
        Csql3=sql3.format(sqlName)
        Csql4=sql4.format(sqlName)
        conn=pymysql.connect(host='localhost',user='root',password='123456',db='processedmobike',charset='utf8')
        cur=conn.cursor()
        cur.execute(Csql3)
        for item in self.B_CDT:
            #print(item[0],item[1],item[5])
            cur.execute(Csql4,(str(item[0]),str(item[1]),str(item[2]),str(item[3]),str(item[4]),str(item[5]),str(item[6])))
        conn.commit()
        cur.close()
        conn.close()

    def GetFocus(self,MinJ,MinW,MaxJ,MaxW,offset,sqlName,sql5):
        conn=pymysql.Connect(host='localhost', user='root',password='123456',db='processedmobike',charset='utf8')
        cur=conn.cursor()
        sql5=sql5.format(sqlName)
        cur.execute(sql5)
        result=cur.fetchall()
        data1=np.array(result)
        data1=data1.astype('float64')
        JSize=int((MaxJ-MinJ)/offset)+1
        WSize=int((MaxW-MinW)/offset)+1
        FlowOut=np.zeros((JSize,WSize))
        FlowIn=np.zeros((JSize,WSize))
        num=0
        count=0
        LonLatOut1=list()
        LonLatIn1=list()
        for item in data1:
            if item[6]!=100:
                if np.abs(item[1]-item[3])>=1e-6 or np.abs(item[2]-item[4])>=1e-6:
                    num=num+1
                    outX=int((item[1]-MinJ)/offset)
                    outY=int((item[2]-MinW)/offset)
                    LonLatOut1.append([item[0],item[1],item[2]])
                    if(outX>=JSize):
                        outX=JSize-1
                    if(outY>=WSize):
                        outY=WSize-1
                    if(outX<0):
                        outX=0
                    if(outY<0):
                        outY=0
                    FlowOut[outX,outY]=FlowOut[outX,outY]+1
                    inX=int((item[3]-MinJ)/offset)
                    inY=int((item[4]-MinW)/offset)
                    LonLatIn1.append([item[0],item[3],item[4]])
                    if(inX>=JSize):
                        inX=JSize-1
                    if(inY>=WSize):
                        inY=WSize-1
                    if(inX<0):
                        inX=0
                    if(inY<0):
                        inY=0
                    FlowIn[inX,inY]=FlowIn[inX,inY]+1
        grid=self.GetGrid(MinJ,MinW,MaxJ,MaxW,offset)
        #return FlowIn,FlowOut,LonLatIn,LonLatOut
        LonLatIn1=np.array(LonLatIn1)
        LonLatOut1=np.array(LonLatOut1)
        self.MakeFile(r'../AllJson/'+sqlName+'IN.json',grid,FlowIn)
        self.MakeFile(r'../AllJson/'+sqlName+'OUT.json',grid,FlowOut)
        self.MakeLocFile(r'../withlonlat/'+sqlName+'OUTP.txt',LonLatOut1)
        self.MakeLocFile(r'../withlonlat/'+sqlName+'INP.txt',LonLatIn1)
        self.flag=0
        self.APCluster(LonLatIn1,sqlName+'IN')
        self.flag=0
        self.APCluster(LonLatOut1,sqlName+'OUT')
        InC=sqlName+'INAPC.txt'
        InP=sqlName+'INAP.txt'
        OutC=sqlName+'OUTAPC.txt'
        OutP=sqlName+'OUTAP.txt'
        self.FocusFlow1(InC,InP,OutC,OutP)


    def MakeFile(self,filepath,data1,data2):
        f=open(filepath,'w')
        f.write('{"heatData":')
        f.write('[')
        i=0
        size=data1.shape[0]
        for item in data2:
            for n in item:
                f.write('{"value":"'+str(int(n))+'","lng":"'+str(data1[i,0])+'","lat":"'+str(data1[i,1])+'"}')
                if i<size-1:
                    f.write(',')
                i=i+1
        f.write(']}')
        f.close()

    def MakeLocFile(self,filepath,data):
        f=open(filepath,'w')
        for item in data:
            f.write(str(item[0])+'\t'+str(item[1])+'\t'+str(item[2])+'\n')
        f.close()


    def GetGrid(self,MinJ,MinW,MaxJ,MaxW,offset):
        xx,yy=np.mgrid[MinJ:MaxJ:offset,MinW:MaxW:offset]
        grid=np.c_[xx.ravel(),yy.ravel()]
        return grid

    def APCluster(self,data1,sqlName):
        i=3000
        while True:
            if i<data1.shape[0]:
                X=np.array(data1[i-3000:i,1:])
                Y=np.array(data1[i-3000:i])
                ap = AffinityPropagation(damping=0.7, max_iter=200, convergence_iter=15, copy=True, preference=None).fit(X)
                self.WriteAPCenters(ap,Y,r'../APCenters/'+sqlName+'APC.txt')
                self.WriteAPResult(ap,Y,r'../APResult/'+sqlName+'AP.txt')
            else:
                X=np.array(data1[i-3000:,1:])
                Y=np.array(data1[i-3000:i])
                ap = AffinityPropagation(damping=0.7, max_iter=200, convergence_iter=15, copy=True, preference=None).fit(X)
                self.WriteAPCenters(ap,Y,r'../APCenters/'+sqlName+'APC.txt')
                self.WriteAPResult(ap,Y,r'../APResult/'+sqlName+'AP.txt')
                break
            i=i+3000

    def WriteAPCenters(self,ap,X,filename):
        f1=open(filename,'a+')
        for item in ap.cluster_centers_:
            f1.write(str(item[0])+'\t'+str(item[1])+'\n')
        f1.close()

    def WriteAPResult(self,ap,X,filename):
        f=open(filename,'a+')
        for i in range(0,X.shape[0]):
            f.write(str(X[i][0])+'\t'+str(X[i][1])+'\t'+str(X[i][2])+'\t'+str(ap.labels_[i]+self.flag)+'\n')
        self.flag=self.flag+ap.cluster_centers_.shape[0]
        f.close()




    def DrawAPResult(self,ap,X):
        cluster_centers_indices = ap.cluster_centers_indices_ # 预测出的中心点的索引，如[123,23,34]
        cluster_centers=np.array(ap.cluster_centers_)
        labels = ap.labels_    # 预测出的每个数据的类别标签,labels是一个NumPy数组
        print(cluster_centers_indices,cluster_centers,labels)
        classnum=cluster_centers.shape[0]
        symbolnum=symbol.shape[0]
        colornum=color.shape[0]
        randsymbol=np.random.randint(0,symbolnum,classnum)
        randcolor=np.random.randint(0,colornum,classnum)
        for i in range(X.shape[0]):
            pointsymble=symbol[randsymbol[labels[i]]]
            pointclolor=color[randcolor[labels[i]]]
            plt.scatter(X[i,0],X[i,1],marker = pointsymble,color = pointclolor, s = 20)
        plt.scatter(cluster_centers[:,0],cluster_centers[:,1],marker='o',color='red')
        plt.show()


    def ReadCenter(self,filename):
        f=open(filename,'r')
        centers=list()
        line=f.readline()
        while line!='':
            line=line[0:-1]
            s=line.split('\t')
            centers.append([s[0],s[1]])
            line=f.readline()
        f.close()
        return np.array(centers)

    def ReadPoint(self,filename):
        f=open(filename,'r')
        labels=list()
        line=f.readline()
        while line!='':
            line=line[0:-1]
            s=line.split('\t')
            labels.append(s[3])
            line=f.readline()
        f.close()
        return np.array(labels)




    def FocusFlow1(self,InC,InP,OutC,OutP):
        InC1=r'../APCenters/'+InC
        InP1=r'../APResult/'+InP
        OutC1=r'../APCenters/'+OutC
        OutP1=r'../APResult/'+OutP
        centers1=self.ReadCenter(InC1)
        labels1=self.ReadPoint(InP1)
        labels2=self.ReadPoint(OutP1)
        centers2=self.ReadCenter(OutC1)
        classnum1=centers1.shape[0]
        classnum2=centers2.shape[0]
        FFCondition=np.zeros((classnum1,classnum2))
        Innum=list()
        Outnum=list()
        for i in range(0,classnum1):
            Innum.append(np.sum(labels1==str(i)))
        for i in range(0,classnum2):
            Outnum.append(np.sum(labels2==str(i)))
        for i in range(0,labels1.shape[0]):
            FFCondition[int(labels1[i]),int(labels2[i])]=FFCondition[int(labels1[i]),int(labels2[i])]+1
        self.MakeFFFile1(FFCondition,centers1,centers2,r'../FocusFlowJSON/'+InC)
        self.MakeFFFile2(FFCondition,centers1,centers2,r'../FocusFlowMINE/'+InC)
        return FFCondition,Innum,Outnum

    def MakeFFFile2(self,FFC,centers1,centers2,filepath):
        point1=np.array(centers1)
        point2=np.array(centers2)
        file=open(filepath,'w')
        for i in range(0,FFC.shape[0]):
            x1=point1[i,0]
            y1=point1[i,1]
            for j in range(0,FFC.shape[1]):
                x2=point2[j,0]
                y2=point2[j,1]
                if FFC[i,j]!=0:
                    width=FFC[i,j]
                    file.write(str(width)+'\t'+str(x2)+'\t'+str(y2)+'\t'+str(x1)+'\t'+str(y1)+'\n')
        file.close()

    def MakeFFFile1(self,FFC,centers1,centers2,filepath):
        point1=np.array(centers1)
        point2=np.array(centers2)
        file=open(filepath,'w')
        file.write('{"Data":[')
        for i in range(0,FFC.shape[0]):
            x1=point1[i,0]
            y1=point1[i,1]
            for j in range(0,FFC.shape[1]):
                x2=point2[j,0]
                y2=point2[j,1]
                if FFC[i,j]!=0:
                    width=FFC[i,j]/10.0
                    file.write('{"value":"'+str(width)+'","lng1":"'+str(x2)+'","lat1":"'+str(y2)+'","lng2":"'+str(x1)+'","lat2":"'+str(y1)+'"}')
                    if i*j<(FFC.shape[0]-1)*(FFC.shape[1]-1):
                        file.write(',')
        file.write(']}')
        file.close()




    def FocusFlow(self,ap1,ap2):
        indices1=ap1.cluster_centers_indices_
        centers1=np.array(ap1.cluster_centers_)
        labels1=np.array(ap1.labels_)
        labels2=np.array(ap2.labels_)
        centers2=np.array(ap2.cluster_centers_)
        classnum1=centers1.shape[0]
        classnum2=centers2.shape[0]
        FFCondition=np.zeros((classnum1,classnum2))
        Innum=list()
        Outnum=list()
        for i in range(0,classnum1):
            Innum.append(np.sum(labels1==i))
        for i in range(0,classnum2):
            Outnum.append(np.sum(labels2==i))
        for i in range(0,labels1.shape[0]):
            FFCondition[labels1[i],labels2[i]]=FFCondition[labels1[i],labels2[i]]+1
        #print(FFCondition,Innum,Outnum)
        return FFCondition,Innum,Outnum

    def DrawFFresult(self,FFC,ap1,ap2):
        point1=np.array(ap1.cluster_centers_)
        point2=np.array(ap2.cluster_centers_)
        for i in range(0,FFC.shape[0]):
            x1=point1[i,0]
            y1=point1[i,1]
            plt.scatter(x1,y1,c='blue',label='流入中心')
            for j in range(0,FFC.shape[1]):
                x2=point2[j,0]
                y2=point2[j,1]
                if FFC[i,j]!=0:
                    width=FFC[i,j]/15.0
                    plt.plot([x1,x2],[y1,y2],c='red',lw=width,label='流动方向，线宽表示自行车数目')
                    plt.scatter(x2,y2,c='green',label='流出中心')
        plt.show()


    def MakeFFFile(self,FFC,ap1,ap2,filepath):
        point1=np.array(ap1.cluster_centers_)
        point2=np.array(ap2.cluster_centers_)
        file=open(filepath,'w')
        file.write('{"Data":[')
        for i in range(0,FFC.shape[0]):
            x1=point1[i,0]
            y1=point1[i,1]
            for j in range(0,FFC.shape[1]):
                x2=point2[j,0]
                y2=point2[j,1]
                if FFC[i,j]!=0:
                    width=FFC[i,j]/15.0
                    file.write('{"value":"'+str(width)+'","lng1":"'+str(x2)+'","lat1":"'+str(y2)+'","lng2":"'+str(x1)+'","lat2":"'+str(y1)+'"}')
                    if i*j<(FFC.shape[0]-1)*(FFC.shape[1]-1):
                        file.write(',')
        file.write(']}')
        file.close()










def GetData(sql):
    conn=pymysql.Connect(host='localhost', user='root',password='123456',db='processedmobike',charset='utf8')
    cur=conn.cursor()
    cur.execute(sql)
    result=cur.fetchall()
    data1=np.array(result)
    #print(data1[:,1:3].astype('float64')[:,:])
    data1[:,1:3]=data1[:,1:3].astype('float64')[:,:]
    #print(data1[:,0][0:-1])
    i=0
    for item in data1:
        #print(item[0][0:-1])
        data1[i][0]=item[0][0:-1]
        i=i+1
    cur.close()
    conn.close()
    return data1

def run():
    #Testx1=GetData(sql1)
    #Testx2=GetData(sql2)
    #print(Testx1,Testx2)
    sql1='''SELECT bikeid,distX,distY FROM {}'''
    sql2='''SELECT bikeid,distX,distY FROM {}'''
    for i in range(0,dataName.shape[0]-1):
        sql1=sql1.format(dataName[i])
        sql2=sql2.format(dataName[i+1])
        Testx1=GetData(sql1)
        Testx2=GetData(sql2)
        K=KNNFLOW(X,Testx1,Testx2)
        flowCondition,bikeCondition=K.BikeFlow()
        sqlname=dataName[i][0:10]+'match'+dataName[i][10:]+dataName[i+1][10:]
        K.SaveData(sqlname)
        K.GetFocus(114.305,30.479,114.447,30.598,0.001,sqlname,sql5)
        sql1='''SELECT bikeid,distX,distY FROM {}'''
        sql2='''SELECT bikeid,distX,distY FROM {}'''
    #K.FocusFlow1(r'jianghan711match07300830INAPC.txt',r'jianghan711match07300830INAP.txt',r'jianghan711match07300830OUTAPC.txt',r'jianghan711match07300830OUTAP.txt')



    #for i in range(0,dataName.shape[0]-1):
    #    sqlnamw=dataName[i][0:11]+'match'+dataName[i][11:]+dataName[i+1][11:]


    #K=KNNFLOW(X,Testx1,Testx2)
   # flowCondition,bikeCondition=K.BikeFlow()
    #print(flowCondition.astype('int'),bikeCondition)
    #K.SaveData('jianghan713match1113')

    #Flowin,Flowout,LonLatin,LonLatout=K.GetFocus(114.194,30.567,114.324,30.657,0.001,'jianghan713match1113',sql5)
    #ap1,X1=K.APCluster(LonLatin)
    #ap2,X2=K.APCluster(LonLatout)
    #FFC,IN,ON=K.FocusFlow(ap1,ap2)
    #K.MakeFFFile(FFC,ap1,ap2,r'../AllJson/FFJson.json')



run()

