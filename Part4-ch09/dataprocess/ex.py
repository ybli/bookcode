import json
import numpy as np
def ex(ID):
    f1=open(r'D:/python代码/MOBIKE/EveryDayFlow/'+ID+'.txt','r')
    S=f1.readlines()
    L=[]
    Flow=np.zeros(6)
    for item in S:
        s=json.loads(item)
        EveryData=s['data']
        L.append(EveryData)
        for i in range(0,6):
            Flow[i]=Flow[i]+EveryData[i][0]+EveryData[i][1]
    FlowRatio=Flow/np.sum(Flow)
    AllData={'Flow':[],'Ratio':[],'POI':[],'Predition':[],'ZombieNum':'','Zombie':[]}
    AllData['Flow']=L
    AllData['Ratio']=FlowRatio
    f1.close()
    f2=open(r'D:/python代码/MOBIKE/POI/'+ID+'.txt','r')
    S=f2.readlines()[0]
    POI=json.loads(S)['Data']
    AllData['POI']=POI
    f2.close()
    f3=open(r'D:/python代码/MOBIKE/predict/'+ID+'.txt','r')
    S=f3.readlines()[0]
    P=json.loads(S)['data']
    AllData['Predition']=P
    f3.close()
    f4=open(r'D:/python代码/MOBIKE/Zombie/'+ID+'.txt','r')
    S=f4.readlines()[0]
    zombie=json.loads(S)['Bikes']
    AllData['Zombie']=zombie
    AllData['ZombieNum']=np.array(zombie).shape[0]
    print(AllData)

ex('mine')




