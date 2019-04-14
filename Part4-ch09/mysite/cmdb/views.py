from django.shortcuts import render
from django.shortcuts import HttpResponse
from django.http import JsonResponse
import json
from cmdb.models import employee
import requests as res
import numpy as np
# Create your views here.

def StaticMap(request):#获取静态热度图数据
    P=request.GET.get("position",None)
    D=request.GET.get('date',None)
    T1=request.GET.get('time1',None)
    T2=request.GET.get('time2',None)
    type=request.GET.get('flow',None)
    filepath=r'./AllJson/'+P+D+'match'+T1+T2+type+'.json'
    #filepath=r'./AllJson/ex.json'
    with open(filepath,'r') as f:
        resp=f.readlines()
    d=json.loads(resp[0])
    response=HttpResponse(json.dumps(d))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def DynamicMap(request):#获取动态热度图数据
    P=request.GET.get("position",None)
    D=request.GET.get('date',None)
    T1=request.GET.get('time1',None)
    T2=request.GET.get('time2',None)
    if int(T1)<7:
        T1=7
    L=int(T2)-int(T1)
    t1=int(T1)
    t2=int(T1)+1
    All={'AllData':[]}
    for i in range(0,L):
        if t1<10:
            tt1='0'+str(t1)
        else:
            tt1=str(t1)
        if t2<10:
            tt2='0'+str(t2)
        else:
            tt2=str(t2)
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'OUT.json'
        with open(filepath,'r') as f:
            resp=f.readlines()
        d=json.loads(resp[0])
        All['AllData'].append(d)
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'IN.json'
        with open(filepath,'r') as f:
            resp=f.readlines()
        d=json.loads(resp[0])
        All['AllData'].append(d)
        t1=t1+1
        t2=t2+1
    response=HttpResponse(json.dumps(All))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response






def login(request):#返回登陆页面
    return render(request,"login.html")

def line(request):#返回流动划线页面
    return render(request,"line.html")

def MoveMap(request):#返回动态热度图页面
    return render(request,"heatmap-move.html")

def check(request):#登陆身份验证，根据请求信息检测数据库
    names=employee.objects.all().values()
    if request.method=="GET":
        user=request.GET.get("user",None)
        password=request.GET.get("password",None)
        flag=0
        for item in names:
            if item['password']==password and item['username']==user:
                flag=1
                break
        if flag==1:
            return HttpResponse('1')
        else:
            return HttpResponse('0')

def register(request):
    try:
        user=request.GET.get("user",None)
        password1=request.GET.get("password",None)
        employee.objects.create(username=user,password=password1)
        return HttpResponse('1')
    except:
        return HttpResponse('0')
def reg(request):
    return render(request,'register.html')

def Menu(request):#返回左侧菜单栏
    return render(request,'menu.html')

def heatmap(request):#返回热度图页面
    return render(request,'heatmap.html')

def index(request):#返回主页面
    return render(request,'index.html')

def GetCenter(request):#返回单车聚类结果及流动信息
    P=request.GET.get("position",None)
    D=request.GET.get('date',None)
    T1=request.GET.get('time1',None)
    T2=request.GET.get('time2',None)
    filepath=r'./FocusFlowJSON/'+P+D+'match'+T1+T2+'INAPC.txt'
    with open(filepath,'r') as f:
        resp=f.readlines()
    d=json.loads(resp[0])
    response=HttpResponse(json.dumps(d))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

headers={
"Host": "mwx.mobike.com",
"rawData": "%7B%22nickName%22%3A%22%40_%40%22%2C%22gender%22%3A1%2C%22language%22%3A%22zh_CN%22%2C%22city%22%3A%22Shenzhen%22%2C%22province%22%3A%22Guangdong%22%2C%22country%22%3A%22China%22%2C%22avatarUrl%22%3A%22https%3A%2F%2Fwx.qlogo.cn%2Fmmopen%2Fvi_32%2F1Sa22gUuyb9ic7AyricWKRkr5SiatfwNicDYvJYicCwtSKibHlyMmAbABD4icXMN5UvUS0gV1ohKs3l3Z1pNDjtofbI3w%2F132%22%7D",
"time": "1537371586117",
"lang": "zh",
"User-Agent": "Mozilla/5.0 (iPhone; CPU iPhone OS 10_2_1 like Mac OS X) AppleWebKit/602.4.6 (KHTML, like Gecko) Mobile/14D27 MicroMessenger/6.6.7 NetType/4G Language/zh_CN",
"Referer": "https://servicewechat.com/wx80f809371ae33eda/333/page-frame.html",
"locationTime": "1537371580793",
"opensrc": "list",
"wxapp": "1",
"latitude": "30.52434",
"signature": "052e7099654c4e624bf1689b4dd3e917a2c54def",
"mainSource": "4003",
"accesstoken": "09e10cc03be50da2b91fba09242bc1d9-0",
"Content-Length": "145",
"avatarUrl": "https://wx.qlogo.cn/mmopen/vi_32/1Sa22gUuyb9ic7AyricWKRkr5SiatfwNicDYvJYicCwtSKibHlyMmAbABD4icXMN5UvUS0gV1ohKs3l3Z1pNDjtofbI3w/132",
"platform": "3",
"Connection": "keep-alive",
"longitude": "114.3549",
"Accept-Language": "zh-cn",
"eption": "42ddb",
"Accept": "*/*",
"Content-Type": "application/x-www-form-urlencoded",
"citycode": "027",
"Accept-Encoding": "gzip, deflate",
"subSource":""
}
def SpiderHandle(request):#返回爬虫页面
    return render(request,'spider.html')

def Spider(request):#返回爬取到的单车分布数据
    initLon=float(request.GET.get('lng',None))-0.005
    initLat=float(request.GET.get('lat',None))-0.005
    offset=0.0025
    CurrentLon=initLon
    CurrentLat=initLat
    MaxLon=initLon+0.01
    MaxLat=initLat+0.01
    data={'spider':[]}
    url = "https://mwx.mobike.com/mobike-api/rent/nearbyBikesInfo.do"
    while CurrentLon<MaxLon:
        CurrentLon=CurrentLon+offset
        CurrentLat=initLat
        while CurrentLat<MaxLat:
            CurrentLat=CurrentLat+offset
            headers['longitude']=str(CurrentLon)
            headers['latitude']=str(CurrentLat)
            payload="latitude=%s&longitude=%s&errMsg=getMapCenterLocation" % (CurrentLat,CurrentLon)
            response=res.request("POST",url,data=payload,headers=headers,verify=False)
            print(response.status_code)
            decoded=json.loads(response.text)['object']
            for x in decoded:
                data['spider'].append({'lon':x['distX'],'lat':x['distY']})
    response=HttpResponse(json.dumps(data))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response


def list(request):#返回面积折线图页面
    return render(request,'list.html')


def Buffer(request):#返回单车流入流出数据
    initLng=float(request.GET.get('lng',None))
    initLat=float(request.GET.get('lat',None))
    D=request.GET.get('date',None)
    P=''
    t1=7
    t2=22
    data={'Data':[]}
    indata=[]
    outdata=[]
    outValues=0
    inValues=0
    if initLng>114.13 and initLng<114.29 and initLat>30.4 and initLat<30.6:
        baseLng=114.13
        baseLat=30.4
        column=201
        P='hanyang'
        if initLng<114.135:
            initLng=initLng+0.005
        if initLng>114.285:
            initLng=initLng-0.005
        if initLat<30.405:
            initLat=initLat+0.005
        if initLat>30.595:
            initLat=initLat-0.005
    elif initLng>114.194 and initLng<114.34 and initLat>30.567 and initLat<30.661:
        baseLng=114.194
        baseLat=30.567
        column=95
        P='jianghan'
        if initLng<114.199:
            initLng=initLng+0.005
        if initLng>114.335:
            initLng=initLng-0.005
        if initLat<30.572:
            initLat=initLat+0.005
        if initLat>30.656:
            initLat=initLat-0.005
    elif initLng>114.305 and initLng<114.446 and initLat>30.479 and initLat<30.597:
        baseLng=114.305
        baseLat=30.479
        column=119
        P='guanggu'
        if initLng<114.31:
            initLng=initLng+0.005
        if initLng>114.441:
            initLng=initLng-0.005
        if initLat<30.484:
            initLat=initLat+0.005
        if initLat>30.592:
            initLat=initLat-0.005
    else:
        return('0')
    for z in range(0,15):
        outValues=0
        inValues=0
        if t1<10:
            tt1='0'+str(t1)
        else:
            tt1=str(t1)
        if t1+1<10:
            tt2='0'+str(t1+1)
        else:
            tt2=str(t1+1)
        t1=t1+1
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'OUT.json'
        outValues=GetValues(filepath,initLng,baseLng,initLat,baseLat,column)
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'IN.json'
        inValues=GetValues(filepath,initLng,baseLng,initLat,baseLat,column)
        indata.append(int(inValues))
        outdata.append(int(outValues))
    data['Data'].append({'in':indata,'out':outdata})
    response=HttpResponse(json.dumps(data))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def GetValues(filepath,initLng,baseLng,initLat,baseLat,column):#读取文件函数
    f=open(filepath,'r')
    s=json.loads(f.readlines()[0])
    d=s['heatData']
    i=int((initLng-baseLng)*1000)
    j=int((initLat-baseLat)*1000)
    outValues=0
    for k in range(0,10):
	    m=i+k-5
	    for l in range(0,10):
		    n=j+l-5
		    outValues=int(d[m*column+n]['value'])+outValues
    return outValues




def Meituan(request):#返回美团分析数据
    initLng=float(request.GET.get('lng',None))
    initLat=float(request.GET.get('lat',None))
    D=request.GET.get('date',None)
    P=''
    data={'Data':[]}
    if initLng>114.13 and initLng<114.29 and initLat>30.4 and initLat<30.6:
        baseLng=114.13
        baseLat=30.4
        column=201
        P='hanyang'
        if initLng<114.135:
            initLng=initLng+0.005
        if initLng>114.285:
            initLng=initLng-0.005
        if initLat<30.405:
            initLat=initLat+0.005
        if initLat>30.595:
            initLat=initLat-0.005
    elif initLng>114.194 and initLng<114.34 and initLat>30.567 and initLat<30.661:
        baseLng=114.194
        baseLat=30.567
        column=95
        P='jianghan'
        if initLng<114.199:
            initLng=initLng+0.005
        if initLng>114.335:
            initLng=initLng-0.005
        if initLat<30.572:
            initLat=initLat+0.005
        if initLat>30.656:
            initLat=initLat-0.005
    elif initLng>114.305 and initLng<114.446 and initLat>30.479 and initLat<30.597:
        baseLng=114.305
        baseLat=30.479
        column=119
        P='guanggu'
        if initLng<114.31:
            initLng=initLng+0.005
        if initLng>114.441:
            initLng=initLng-0.005
        if initLat<30.484:
            initLat=initLat+0.005
        if initLat>30.592:
            initLat=initLat-0.005
    t1=7
    t2=20
    morning=0
    noon=0
    afternoon=0
    night=0
    for z in range(0,13):
        outValues=0
        inValues=0
        if t1<10:
            tt1='0'+str(t1)
        else:
            tt1=str(t1)
        if t1+1<10:
            tt2='0'+str(t1+1)
        else:
            tt2=str(t1+1)
        t1=t1+1
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'OUT.json'
        outValues=GetValues(filepath,initLng,baseLng,initLat,baseLat,column)
        filepath=r'./AllJson/'+P+D+'match'+tt1+tt2+'IN.json'
        inValues=GetValues(filepath,initLng,baseLng,initLat,baseLat,column)
        if z>=0 and z<3:
            morning=morning+inValues-outValues
        elif z>=3 and z<6:
            noon=noon+inValues-outValues
        elif z>=6 and z<10:
            afternoon=afternoon+inValues-outValues
        else:
            night=night+inValues-outValues
    morning1=morning/270
    noon1=noon/270
    afternoon1=afternoon/360
    night1=night/270
    filepath=r'./meituan/'+P+'.txt'
    f=open(filepath,'r')
    s=json.loads(f.readlines()[0])
    d=s['Meituan']
    shopnum=0
    i=int((initLng-baseLng)*1000)
    j=int((initLat-baseLat)*1000)
    for k in range(0,10):
	    m=i+k-5
	    for l in range(0,10):
		    n=j+l-5
		    shopnum=float(d[m*column+n]['value'])*10+shopnum
    heat=[]
    shopnum1=shopnum
    shopnum=shopnum/200
    heat.append(30*(np.abs(morning1)*0.6+shopnum*0.4))
    heat.append(30*(np.abs(noon1)*0.6+shopnum*0.4))
    heat.append(30*(np.abs(afternoon1)*0.6+shopnum*0.4))
    heat.append(30*(np.abs(night1)*0.6+shopnum*0.4))
    data['Data'].append({'heat':heat})
    data['Data'].append({'flow':[int(morning/3+0.5),int(noon/3+0.5),int(afternoon/4+0.5),int(night/3+0.5)]})
    data['Data'].append({'meituan':shopnum1})
    response=HttpResponse(json.dumps(data))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def radar(request):#返回雷达图页面
    return render(request,'radar.html')

def MeituanPoint(request):#返回美团商家数据
    P=request.GET.get('position',None)
    D=request.GET.get('date',None)
    if P=='guanggu':
        baseLng=114.305
        baseLat=30.479
        maxLng=114.447
        maxLat=30.597
        column=119
    if P=='hanyang':
        baseLng=114.13
        baseLat=30.4
        maxLng=114.29
        maxLat=30.6
        column=201
    if P=='jianghan':
        baseLng=114.194
        baseLat=30.567
        maxLng=114.34
        maxLat=30.661
        column=95
    filepath=r'./meituan/'+P+'center.txt'
    f=open(filepath,'r')
    data=json.loads(f.readlines()[0])
    meituan=data['Data']
    center=meituan[1]['Center']
    noonflowin={'NoonFlowIn':[]}
    filepath=r'./AllJson/'+P+D+'match'+'12'+'13'+'IN.json'
    f=open(filepath,'r')
    s=json.loads(f.readlines()[0])
    d=s['heatData']
    for item in center:
        inValues=0
        initLng=float(item[0])
        initLat=float(item[1])
        if initLng<baseLng+0.005:
            initLng=initLng+0.005
        if initLng>maxLng-0.005:
            initLng=initLng-0.05
        if initLat<baseLat+0.005:
            initLat=initLat+0.005
        if initLat>maxLat-0.005:
            initLat=initLat-0.005
        i=int((initLng-baseLng)*1000)
        j=int((initLat-baseLat)*1000)
        for k in range(0,10):
	        m=i+k-5
	        for l in range(0,10):
		        n=j+l-5
		        inValues=int(d[m*column+n]['value'])+inValues
        noonflowin['NoonFlowIn'].append(inValues)
    nightflowin={'NightFlowIn':[]}
    filepath=r'./AllJson/'+P+D+'match'+'18'+'19'+'IN.json'
    f=open(filepath,'r')
    s=json.loads(f.readlines()[0])
    d=s['heatData']
    for item in center:
        inValues=0
        initLng=float(item[0])
        initLat=float(item[1])
        if initLng<baseLng+0.005:
            initLng=initLng+0.005
        if initLng>maxLng-0.005:
            initLng=initLng-0.005
        if initLat<baseLat+0.005:
            initLat=initLat+0.005
        if initLat>maxLat-0.005:
            initLat=initLat-0.005
        i=int((initLng-baseLng)*1000)
        j=int((initLat-baseLat)*1000)
        for k in range(0,10):
	        m=i+k-5
	        for l in range(0,10):
		        n=j+l-5
		        inValues=int(d[m*column+n]['value'])+inValues
        nightflowin['NightFlowIn'].append(inValues)
    data['Data'].append(noonflowin)
    data['Data'].append(nightflowin)
    response=HttpResponse(json.dumps(data))
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def MeituanA(request):#返回美团页面
    return render(request,'meituan.html')


def AllDay(request):#返回整天的数据
    P=request.GET.get('position',None)
    D=request.GET.get('date',None)
    fp1=r'./AllDay/'+P+'.txt'
    f1=open(fp1,'r')
    s1=json.loads(f1.readlines()[0])
    d1=s1['Data']
    fp2=r'./AllDay/'+P+D+'.txt'
    f2=open(fp2,'r')
    s2=json.loads(f2.readlines()[0])
    d2=s2['Data']
    d1.append(d2)
    data={'Data':d1}
    response=HttpResponse(json.dumps(data,ensure_ascii=False),content_type='application/json;charset=utf-8')
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def Zonghe(request):#返回综合数据
    return render(request,'zonghe.html')

def Point(request):#返回页面
    return render(request,'point.html')

def ThreeD(request):
    return render(request,'heatmap-3D.html')

def test():
    print('test')

def UserPoint(request):
    try:
        lat=request.GET.get('lat',None)
        lon=request.GET.get('lng',None)
        SD=request.GET.get('startdate',None)
        ED=request.GET.get('enddate',None)
        ID=request.GET.get('id',None)
        f=open(r'./UserPoint/'+ID+'.txt','w')
        Data={'Info':{"Lon":'',"Lat":'',"ID":'',"SD":'',"ED":''}}
        Data['Info']["Lon"]=lon
        Data['Info']["Lat"]=lat
        Data['Info']["ID"]=ID
        Data['Info']["SD"]=SD
        Data['Info']['ED']=ED
        json.dump(Data,f)
        return HttpResponse('1')
    except:
        return HttpResponse('0')

def zombie(request):
    return render(request,'zombie.html')

def GetZombie(request):
    P=request.GET.get('position',None)
    f=open(r'./new/new/'+P+'.json','r')
    L=f.readlines()[0]
    s=json.loads(L)
    response=HttpResponse(json.dumps(s,ensure_ascii=False),content_type='application/json;charset=utf-8')
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def Route(request):
    return render(request,'route.html')

def GetRoute(request):
    f=open(r'./new/new/routedata.json','r')
    L=f.readlines()[0]
    s=json.loads(L)
    response=HttpResponse(json.dumps(s,ensure_ascii=False),content_type='application/json;charset=utf-8')
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def Record(request):
    return render(request,'record.html')

def GiveData(request):
    ID=request.GET.get('ID',None)
    f1=open(r'./EveryDayFlow/'+ID+'.txt','r')
    S=f1.readlines()
    L=[]
    MAX=[]
    Judge=np.zeros(6)
    Flow=np.zeros(6)
    for item in S:
        s=json.loads(item)
        EveryData=s['data']
        L.append(EveryData)
        for i in range(0,6):
            Flow[i]=Flow[i]+EveryData[i][0]+EveryData[i][1]
            Judge[i]=EveryData[i][0]+EveryData[i][1]
        MAX.append(int(np.where(Judge==np.max(Judge))[0][0]))
    Mymax=[]
    for item in MAX:
        if item==0:
            Mymax.append('7-8')
        elif item==1:
            Mymax.append('8-9')
        elif item==2:
            Mymax.append('11-12')
        elif item==3:
            Mymax.append('12-13')
        elif item==4:
            Mymax.append('17-18')
        elif item==5:
            Mymax.append('18-19')
    FlowRatio=Flow/np.sum(Flow)
    FR=[]
    for item in FlowRatio:
        FR.append(item)
    AllData={'Flow':[],'Ratio':[],'POI':[],'Predition':[],'ZombieNum':'','Zombie':[],'Max':[],'True':[]}
    AllData['Flow']=L
    AllData['Ratio']=FR
    AllData['Max']=Mymax
    f1.close()
    f2=open(r'./POI/'+ID+'.txt','r')
    S=f2.readlines()[0]
    POI=json.loads(S)['Data']
    AllData['POI']=POI
    f2.close()
    f3=open(r'./predict/'+ID+'.txt','r')
    S=f3.readlines()[0]
    P=json.loads(S)['data']
    AllData['Predition']=P
    f3.close()
    f4=open(r'./Zombie/'+ID+'.txt','r')
    S=f4.readlines()[0]
    zombie=json.loads(S)['Bikes']
    AllData['Zombie']=zombie
    AllData['ZombieNum']=np.array(zombie).shape[0]
    f5=open(r'./True/'+ID+'.txt','r')
    L=f5.readlines()[0]
    S=json.loads(L)['data']
    AllData['True']=S
    f5.close()
    response=HttpResponse(json.dumps(AllData,ensure_ascii=False),content_type='application/json;charset=utf-8')
    response["Access-Control-Allow-Origin"] = "*"
    response["Access-Control-Allow-Methods"] = "POST, GET, OPTIONS"
    response["Access-Control-Max-Age"] = "1000"
    response["Access-Control-Allow-Headers"] = "*"
    return response

def Report(request):
    return render(request,'report.html')
