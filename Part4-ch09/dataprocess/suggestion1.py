import json
import numpy

def GetValues(filepath1):
    f=open(filepath1,'r')
    s=json.loads(f.readlines()[0])
    data=s['heatData']
    values=0
    for item in data:
        values=values+int(item['value'])
    f.close()
    return values

def run():
    L1=['guanggu','hanyang','jianghan']
    L2=['07','08','09','10','11','12','13','14','15','16','17','18','19','20','21','22']
    for item in L1:
        for i in range(11,23):
            fp1=r'../AllDay/'+item+'7'+str(i)+'.txt'
            f1=open(fp1,'w')
            data={'Data':[]}
            for j in range(0,15):
                fp=r'../AllJson/'+item+'7'+str(i)+'match'+L2[j]+L2[j+1]+'OUT.json'
                values=GetValues(fp)
                data['Data'].append(values)
            print(data)
            json.dump(data,f1)
            f1.close()

#run()





def GetHotPoint(filepath1,filepath2,length):
    f=open(filepath1,'r')
    s=json.loads(f.readlines()[0])
    data=s['heatData']
    hotData={"Data":[]}
    i=0
    for item in data:
        if int(item['value'])>6:
            try:
                value1=int(data[i-1]['value'])
                value2=int(data[i-1]['value'])
                value3=int(data[i-length]['value'])
                value4=int(data[i+length]['value'])
            except:
                value1=0
                value2=0
                value3=0
                value4=0
            if value1>3 and value2>3 and value3>3 and value4>3:
                hotData['Data'].append([item['lng'],item['lat']])
                print(item)
        i=i+1
    f1=open(filepath2,'w')
    json.dump(hotData,f1)
    f.close()
    f1.close()

#GetHotPoint(r'../AllJson/jianghan722match0809OUT.json',r'../AllDay/jianghan.txt',95)

f=open(r'../AllDay/hanyang.txt','r')
s=json.loads(f.readlines()[0])
print(s)