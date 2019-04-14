import requests
import numpy as np
import threading
import json
import pymysql
import time
url = "https://mwx.mobike.com/mobike-api/rent/nearbyBikesInfo.do"
headers={
"Host": "mwx.mobike.com",
"mobileNo": "15637979070",
"rawData": "%7B%22nickName%22%3A%22A%22%2C%22gender%22%3A0%2C%22language%22%3A%22zh_CN%22%2C%22city%22%3A%22%22%2C%22province%22%3A%22%22%2C%22country%22%3A%22%22%2C%22avatarUrl%22%3A%22https%3A%2F%2Fwx.qlogo.cn%2Fmmopen%2Fvi_32%2FDYAIOgq83eqdcj9pmjPU9VS5muW6k9kILj3qhvsCRBy3HBzdJmeel3b1GbhjQR0HfNtVMo9zF5IH5eBV62ecFw%2F132%22%7D",
"time": "1530591282085",
"lang": "zh",
"User-Agent": "Mozilla/5.0 (iPhone; CPU iPhone OS 11_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15F79 MicroMessenger/6.7.0 NetType/WIFI Language/zh_CN",
"Referer": "https://servicewechat.com/wx80f809371ae33eda/293/page-frame.html",
"nickName": "A",
"userid": "7403353619929116726272894343",
"opensrc": "list",
"latitude": "30.52853012084961",
"signature": "2e652644a522a0798c291a4870dbf1b4a5e4e97b",
"mainSource": "4003",
"accesstoken": "205964cca6b7966ccd1c0e1a638e7f75-0",
"Content-Length": "229",
"avatarUrl": "https://wx.qlogo.cn/mmopen/vi_32/DYAIOgq83eqdcj9pmjPU9VS5muW6k9kILj3qhvsCRBy3HBzdJmeel3b1GbhjQR0HfNtVMo9zF5IH5eBV62ecFw/132",
"platform": "3",
"Connection": "keep-alive",
"longitude": "114.35778045654297",
"Accept-Language": "zh-cn",
"eption": "90782",
"Accept": "*/*",
"Content-Type": "application/x-www-form-urlencoded",
"citycode": "027",
"wxcode": "081qqml41BDyvM1SlNh41u0Jl41qqmlS",
"subSource":"",
"Accept-Encoding": "br, gzip, deflate"
    }
sql='''
        create table {}(
        bikeId varchar(255) not null,
        distX float(32) not null,
        distY float(32) not null)DEFAULT CHARSET=utf8;
        '''
sqla='''
        insert into {}(bikeId,distX,distY)
        values(%s,%s,%s);
    '''
payload = "latitude=%s&longitude=%s&errMsg=getMapCenterLocation" % (30.5285,114.3577)
from requests.packages.urllib3.exceptions import InsecureRequestWarning
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)
class myThread(threading.Thread):
    def __init__(self,name,BLon,BLat,MLon,MLat):
        threading.Thread.__init__(self)
        self.name=name
        self.BLon=BLon
        self.BLat=BLat
        self.MLon=MLon
        self.MLat=MLat
    def run(self):
        conn=pymysql.connect(host='localhost',user='root',password='123456',db='mysql',charset='utf8')
        cur=conn.cursor()
        CurrentLon=self.BLon
        CurrentLat=self.BLat
        offset=0.002
        MaxLon=self.MLon
        MaxLat=self.MLat
        Csql=sql.format(self.name)
        Csqla=sqla.format(self.name)
        print(Csql,Csqla)
        #try:
        cur.execute(Csql)
        #except:
        #    print('数据库创建失败')
        while CurrentLon<MaxLon:
            CurrentLon=CurrentLon+offset
            CurrentLat=self.BLat
            while CurrentLat<MaxLat:
                time.sleep(0.01)
                CurrentLat=CurrentLat+offset
                headers['longitude']=str(CurrentLon)
                headers['latitude']=str(CurrentLat)
                payload="latitude=%s&longitude=%s&errMsg=getMapCenterLocation" % (CurrentLat,CurrentLon)
                try:
                    response=requests.request("POST",url,data=payload,headers=headers,verify=False)
                    print(self.name,response.status_code)
                    decoded=json.loads(response.text)['object']
                    for x in decoded:
                        cur.execute(Csqla,(x['bikeIds'],x['distX'],x['distY']))
                except:
                    print(CurrentLat,CurrentLon)
        conn.commit()
        cur.close()
        conn.close()

d={"NO7030601":[114.120,30.392,114.35,30.6],
   "NO7030602":[114.35,30.392,114.50,30.6],
   "NO7030603":[114.50,30.392,114.638,30.6],
   "NO7030604":[114.120,30.6,114.35,30.7],
   "NO7030605":[114.35,30.6,114.5,30.7],
   "NO7030606":[114.5,30.6,114.638,30.7]}
threads=[]
for key in d:
    thread=myThread(key,d[key][0],d[key][1],d[key][2],d[key][3])
    threads.append(thread)
for item in threads:
    item.start()
for item in threads:
    item.join()
