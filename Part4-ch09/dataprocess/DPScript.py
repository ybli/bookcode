import time
import json
import numpy as np
import os

def InAndOut(filepathes):
    times=['07','08','09','11','12','13','17','18','19']
    #date=time.strftime('%m%d',time.localtime(time.time()))
    date='0903'
    mydata={'data':[]}
    for item in filepathes:
        f=open(item,'r')
        l=f.readlines()[0]
        s=json.loads(l)
        data=s['Info']
        if int(data['SD'])<int(date):
            i=0
            for i in range(0,8):
                if int(times[i+1])-int(times[i])==1:
                    OutNum=0
                    InNum=0
                    fp1=r'../EveryDayCenter/'+data['ID']+date+times[i]+'.txt'
                    fp2=r'../EveryDayCenter/'+data['ID']+date+times[i+1]+'.txt'
                    s1=np.array(OpenFile(fp1))
                    s2=np.array(OpenFile(fp2))
                    for item in s1:
                        if item[0] not in s2[:,0]:
                            OutNum=OutNum+1
                    for item in s2:
                        if item[0] not in s1[:,0]:
                            InNum=InNum+1
                    mydata['data'].append([OutNum,InNum])
            f=open(r'../EveryDayFlow/'+data['ID']+date+'.txt','w')
            json.dump(mydata,f)

def OpenFile(fp):
    f=open(fp,'r')
    l=f.readlines()[0]
    s=json.loads(l)
    s=s['Data']
    return s

def RunScript():
    while True:
        Now=time.strftime('%H',time.localtime(time.time()))
        if int(Now)==19:
            workdir='D:\\python代码\\MOBIKE\\UserPoint'
            fs=os.listdir(workdir)
            fps=[]
            for item in fs:
                fp=os.path.join(workdir,item)
                fps.append(fp)
            InAndOut(fps)
            #ZombieBike(fps)
            break



def ZombieBike(filepathes):
    date=time.strftime('%m%d',time.localtime(time.time()))
    bikes={'Bikes':[]}
    for item in filepathes:
        f=open(item,'r')
        l=f.readlines()[0]
        s=json.loads(l)
        data=s['Info']
        if data['ED']==date:
            fp1=r'../EveryDayCenter/'+data['ID']+data['SD']+'07'+'.txt'
            fp2=r'../EveryDayCenter/'+data['ID']+data['ED']+'18'+'.txt'
            s1=OpenFile(fp1)
            s2=OpenFile(fp2)
            for item1 in s1:
                for item2 in s2:
                    if item1[0]==item2[0]:
                        if np.abs(float(item1[1])-float(item2[1]))<1e-6 and np.abs(float(item1[2]-float(item2[2])))<1e-6:
                            bikes['Bikes'].append(item1[0])
            f=open(r'../Zombie/'+data['ID']+'.txt','w')
            json.dump(bikes,f)
            f.close()

RunScript()

FakeData=np.array([[123,23,33,243,235,129],
                   [123,23,33,243,235,129],
                   [123,23,33,243,235,129]])

def GetFileData(fp):
    f=open(r'../EveryDayFlow/'+fp+'.txt','r')
    L=f.readlines()[0]
    S=json.loads(L)
    data=S['data']
    IN=[]
    OUT=[]
    for item in data:
        IN.append(item[1])
        OUT.append(item[0])
    return OUT,IN

def GetData(filelist):
    OUT=[]
    IN=[]
    for item in filelist:
        OUT1,IN1=GetFileData(item)
        OUT.append(OUT1)
        IN.append(IN1)
        print(IN)
        print(OUT)
    y1=DataProdict(np.array(OUT).T)
    y2=DataProdict(np.array(IN).T)
    thisdata={'data':[]}
    thisdata['data'].append(y1)
    thisdata['data'].append(y2)
    print(thisdata)
    f=open(r'../predict/mine.txt','w')
    json.dump(thisdata,f)
    f.close()






def DataProdict(data):
    size=data.shape[1]
    a=np.zeros(6)
    b=np.zeros(6)
    alpha=0.3
    st1=np.zeros(size)
    st2=np.zeros(size)
    yjian=np.zeros([6,1])
    for i in range(6):
        st1[0]=data[i][0]
        st2[0]=data[i][0]
        for j in range(1,size):
            st1[j]=alpha*data[i][j]+(1-alpha)*st1[j-1]
            st2[j]=alpha*st1[j]+(1-alpha)*st2[j-1]
        a[i]=2*st1[size-1]-st2[size-1]
        b[i]=alpha/(1-alpha)*(st1[size-1]-st2[size-1])
    for i in range(6):
        yjian[i][0]=int(a[i]+b[i]*j+0.5)
    y=[]
    for item in yjian:
        y.append(item[0])
    return y

#GetData(['mine0830','mine0831','mine0901','mine0902'])