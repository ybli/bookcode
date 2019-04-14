import pymysql
import numpy as np
import json
from sklearn.cluster import AffinityPropagation

sql1='''SELECT Longitude,Latitude FROM {}'''

def getData(sql):
    conn=pymysql.Connect(host='localhost', user='root',password='123456',db='mysql',charset='utf8')
    cur=conn.cursor()
    cur.execute(sql)
    result=cur.fetchall()
    data1=np.array(result)
    cur.close()
    conn.close()
    return data1

def run(MinJ,MinW,MaxJ,MaxW,offset,data,filepath):
    JSize=int((MaxJ-MinJ)/offset)+1
    WSize=int((MaxW-MinW)/offset)+1
    num=np.zeros((JSize,WSize))
    value=0
    P=[]
    for item1 in data:
        for item2 in item1:
            try:
                J=float(item2[0])
                W=float(item2[1])
            except:
                J=0
                W=0
            if J>MinJ and J<MaxJ and W>MinW and W<MaxW:
                outX=int((J-MinJ)/offset)
                outY=int((W-MinW)/offset)
                P.append([J,W])
                if(outX>=JSize):
                    outX=JSize-1
                if(outY>=WSize):
                    outY=WSize-1
                if(outX<0):
                    outX=0
                if(outY<0):
                    outY=0
                num[outX,outY]=num[outX,outY]+1
                value=value+1
    ap = AffinityPropagation(damping=0.7, max_iter=200, convergence_iter=15, copy=True, preference=None).fit(P)
    f=open(r'../meituan/jianghancenter.txt','w')
    data={'Data':[]}
    c=[]
    for item in ap.cluster_centers_:
        c.append([item[0],item[1]])
    data['Data'].append({'AllShop':P})
    data['Data'].append({'Center':c})
    centers=np.array(ap.cluster_centers_)
    size=centers.shape[0]
    shopnum={'ShopNum':[]}
    labels=np.array(ap.labels_)
    print(labels)
    for i in range(0,size):
        count=0
        for item in labels:
            if item==i:
                count=count+1
        shopnum['ShopNum'].append(count)
    data['Data'].append(shopnum)
    data=json.dump(data,f)
    #f.write(data)
    f.close()



def MakeFile(filepath,data1,data2):
    f=open(filepath,'w')
    f.write('{"Meituan":')
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



def GetGrid(MinJ,MinW,MaxJ,MaxW,offset):
        xx,yy=np.mgrid[MinJ:MaxJ:offset,MinW:MaxW:offset]
        grid=np.c_[xx.ravel(),yy.ravel()]
        return grid

d=['hanyangqu','qiaokouqu','jianghanqu','jianganqu','wuchangqu','hongshanqu']

data=[]
for item in d:
    sql1='''SELECT Longitude,Latitude FROM {}'''
    sql1=sql1.format(item)
    data.append(getData(sql1))
MinJ=114.194
MinW=30.567
MaxJ=114.34
MaxW=30.661
offset=0.001
filepath=r'../meituan/hanyang.txt'
run(MinJ,MinW,MaxJ,MaxW,offset,data,filepath)
