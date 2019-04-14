import numpy
import json
def CreatData():
    f1=open(r'../AllJson/guanggu716match1821OUT.json','r')
    s=json.loads(f1.readlines()[0])
    data=s['heatData']
    dataNew1={'heatData':[]}
    dataNew2={'heatData':[]}
    dataNew3={'heatData':[]}
    x1=0
    x2=0
    x3=0
    x4=0
    for item in data:
        value1=int(int(item['value'])*0.36+0.5)
        value2=int(int(item['value'])*0.3+0.5)
        value3=int(item['value'])-value1-value2
        x1=x1+value1
        x2=x2+value2
        x4=x4+value3
        x3=x3+int(item['value'])
        lng=item['lng']
        lat=item['lat']
        dataNew1['heatData'].append({'value':value1,'lng':lng,'lat':lat})
        dataNew2['heatData'].append({'value':value2,'lng':lng,'lat':lat})
        dataNew3['heatData'].append({'value':value3,'lng':lng,'lat':lat})
    f2=open(r'../AllJson1/guanggu716match1819OUT.json','w')
    f3=open(r'../AllJson1/guanggu716match1920OUT.json','w')
    f4=open(r'../AllJson1/guanggu716match2021OUT.json','w')
    json.dump(dataNew1,f2)
    json.dump(dataNew2,f3)
    json.dump(dataNew3,f4)
    f2.close()
    f1.close()
    f3.close()
    f4.close()
    print(x1,x2,x3,x4)


def CreatData1():
    f1=open(r'../AllJson/guanggu716match1517IN.json','r')
    s=json.loads(f1.readlines()[0])
    data=s['heatData']
    dataNew1={'heatData':[]}
    #dataNew2={'heatData':[]}
    dataNew3={'heatData':[]}
    x1=0
    x2=0
    x3=0
    x4=0
    for item in data:
        value1=int(float(item['value'])*0.48+0.5)
        #value2=int(int(item['value'])*0.3+0.5)
        value3=int(item['value'])-value1
        x1=x1+value1
        x2=x2+value3
       # x4=x4+value3
        x3=x3+int(item['value'])
        lng=item['lng']
        lat=item['lat']
        dataNew1['heatData'].append({'value':value1,'lng':lng,'lat':lat})
        #dataNew2['heatData'].append({'value':value2,'lng':lng,'lat':lat})
        dataNew3['heatData'].append({'value':value3,'lng':lng,'lat':lat})
    f2=open(r'../AllJson2/guanggu716match1516IN.json','w')
    #f3=open(r'../AllJson1/guanggu714match1314IN.json','w')
    f4=open(r'../AllJson2/guanggu716match1617IN.json','w')
    json.dump(dataNew1,f2)
    #json.dump(dataNew2,f3)
    json.dump(dataNew3,f4)
    f2.close()
    f1.close()
    #f3.close()
    f4.close()
    print(x1,x2,x3)


def CreatData2():
    f1=open(r'../AllJson/guanggu717match1115OUT.json','r')
    s=json.loads(f1.readlines()[0])
    data=s['heatData']
    dataNew1={'heatData':[]}
    dataNew2={'heatData':[]}
    dataNew3={'heatData':[]}
    dataNew4={'heatData':[]}
    x1=0
    x2=0
    x3=0
    x4=0
    x5=0
    for item in data:
        value1=int(int(item['value'])*0.22+0.5)
        value2=int(int(item['value'])*0.25+0.5)
        value3=int(int(item['value'])*0.27+0.5)
        value4=int(item['value'])-value1-value2-value3
        x1=x1+value1
        x2=x2+value2
        x4=x4+value3
        x5=x5+value4
        x3=x3+int(item['value'])
        lng=item['lng']
        lat=item['lat']
        dataNew1['heatData'].append({'value':value1,'lng':lng,'lat':lat})
        dataNew2['heatData'].append({'value':value2,'lng':lng,'lat':lat})
        dataNew3['heatData'].append({'value':value3,'lng':lng,'lat':lat})
        dataNew4['heatData'].append({'value':value4,'lng':lng,'lat':lat})
    f2=open(r'../AllJson1/guanggu717match1112OUT.json','w')
    f3=open(r'../AllJson1/guanggu717match1213OUT.json','w')
    f4=open(r'../AllJson1/guanggu717match1314OUT.json','w')
    f5=open(r'../AllJson1/guanggu717match1415OUT.json','w')
    json.dump(dataNew1,f2)
    json.dump(dataNew2,f3)
    json.dump(dataNew3,f4)
    json.dump(dataNew4,f5)
    f2.close()
    f1.close()
    f3.close()
    f4.close()
    print(x1,x2,x3,x4,x5)
CreatData1()
