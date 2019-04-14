import requests
import time
import json
import numpy as np
import os
from requests.packages.urllib3.exceptions import InsecureRequestWarning
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)
headers={
"Host": "mwx.mobike.com",
"mobileNo": "15637979070",
"rawData": "%7B%22nickName%22%3A%22A%22%2C%22gender%22%3A0%2C%22language%22%3A%22zh_CN%22%2C%22city%22%3A%22%22%2C%22province%22%3A%22%22%2C%22country%22%3A%22%22%2C%22avatarUrl%22%3A%22https%3A%2F%2Fwx.qlogo.cn%2Fmmopen%2Fvi_32%2FDYAIOgq83eqdcj9pmjPU9VS5muW6k9kILj3qhvsCRBy3HBzdJmeel3b1GbhjQR0Hm4q6eTDMJ5EicA0mt0uoVJw%2F132%22%7D",
"time": "1536405757169",
"lang": "zh",
"User-Agent": "Mozilla/5.0 (iPhone; CPU iPhone OS 11_4_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15G77 MicroMessenger/6.7.1 NetType/WIFI Language/zh_CN",
"Referer": "https://servicewechat.com/wx80f809371ae33eda/327/page-frame.html",
"nickName": "A",
"opensrc": "list",
"userid": "7403353619929116726272894343",
"latitude": "30.528470993041992",
"signature": "9221b72b806009fd09ab50d8356df29daa5c81fb",
"mainSource": "4003",
"accesstoken": "691873063adac1a12bef67f948d3cdfc-0",
"Content-Length": "146",
"avatarUrl": "https://wx.qlogo.cn/mmopen/vi_32/DYAIOgq83eqdcj9pmjPU9VS5muW6k9kILj3qhvsCRBy3HBzdJmeel3b1GbhjQR0Hm4q6eTDMJ5EicA0mt0uoVJw/132",
"platform": "3",
"Connection": "keep-alive",
"longitude": "114.35778045654297",
"Accept-Language": "zh-cn",
"eption": "4e1c8",
"Accept": "*/*",
"Content-Type": "application/x-www-form-urlencoded",
"citycode": "027",
"subSource":"",
"Accept-Encoding": "br, gzip, deflate"
}
def Spider(Lon,Lat,headers):#返回爬取到的单车分布数据
    initLon=float(Lon)-0.005
    initLat=float(Lat)-0.005
    print(initLat,initLon)
    offset=0.0025
    CurrentLon=initLon
    CurrentLat=initLat
    MaxLon=initLon+0.01
    MaxLat=initLat+0.01
    Bikes=[]
    url = "https://mwx.mobike.com/mobike-api/rent/nearbyBikesInfo.do"
    while CurrentLon<MaxLon:
        CurrentLon=CurrentLon+offset
        CurrentLat=initLat
        while CurrentLat<MaxLat:
            time.sleep(0.5)
            CurrentLat=CurrentLat+offset
            headers['longitude']=str(CurrentLon)
            headers['latitude']=str(CurrentLat)
            payload="latitude=%s&longitude=%s&errMsg=getMapCenterLocation" % (CurrentLat,CurrentLon)
            response=requests.request("POST",url,data=payload,headers=headers,verify=False)
            if response.status_code!=200:
                for i in range(3):
                    response=requests.request("POST",url,data=payload,headers=headers,verify=False)
                    if response.status_code==200:
                        break
            print(response.text)
            try:
                decoded=json.loads(response.text)['object']
                for x in decoded:
                    if [x['distX'],x['distY']] not in Bikes:
                        Bikes.append([x['bikeIds'][0:-1],x['distX'],x['distY']])
            except:
                print(response.text)
    return Bikes

Points=[["114.357", "30.566"], ["114.363", "30.569"]]#, ["114.37", "30.52"], ["114.372", "30.531"], ["114.372", "30.574"], ["114.373", "30.518"], ["114.377", "30.534"], ["114.385", "30.505"], ["114.393", "30.511"], ["114.402", "30.498"], ["114.411", "30.528"], ["114.413", "30.53"], ["114.417", "30.537"], ["114.419", "30.541"], ["114.424", "30.489"], ["114.431", "30.512"], ["114.432", "30.514"], ["114.435", "30.505"]]

def EasyBikes(Now):
    mydata={'Data':[]}
    lon=114.357
    lat=30.566
    B=Spider(lon,lat,headers)
    mydata['Data']=B
    mydate=time.strftime('%m%d',time.localtime(time.time()))
    f=open(r'../EveryDayCenter/'+'mine'+mydate+Now+'.txt','w')
    json.dump(mydata,f)
    f.close()

#EasyBikes('11')


def GetBikes(filepathes):
    print(filepathes)
    i=0
    date=int(time.strftime('%m%d',time.localtime(time.time())))
    flag={'23':0,'00':0,'01':0,'03':0,'04':0,'05':0,'09':0,'10':0,'19':0}
    while True:
        Now=time.strftime('%H',time.localtime(time.time()))
        if Now=='23' or Now=='00' or Now=='01' or Now=='03' or Now=='04' or Now=='05' or Now=='09' or Now=='10' or Now=='19':
            #print(Now)
            if flag[Now]==0:
                print(flag)
                flag[Now]=1
                for item in filepathes:
                    print(item)
                    f=open(item,'r')
                    l=f.readlines()[0]
                    s=json.loads(l)
                    data=s['Info']
                    mydata={'Data':[]}
                    if int(data['ED'])>date:
                        lon=data['Lon']
                        lat=data['Lat']
                        B=Spider(lon,lat,headers)
                        mydata['Data']=B
                        mydate=time.strftime('%m%d',time.localtime(time.time()))
                        f=open(r'../EveryDayCenter/'+data['ID']+mydate+Now+'.txt','w')
                        json.dump(mydata,f)
                        f.close()
        if flag['19']==1:
            break

def RunScript():
    while True:
        Now=time.strftime('%H',time.localtime(time.time()))
        if int(Now)>=6:
            workdir='D:\\python代码\\MOBIKE\\UserPoint'
            fs=os.listdir(workdir)
            fps=[]
            for item in fs:
                fp=os.path.join(workdir,item)
                fps.append(fp)
            GetBikes(fps)
            break

RunScript()