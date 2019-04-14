import pymysql
import numpy as np

sql1='''SELECT bikeId,distX,distY FROM {}'''
sql3='''
        create table {}(
        bikeId varchar(255) not null,
        distX float(32) not null,
        distY float(32) not null)DEFAULT CHARSET=utf8;
'''
sql4='''
        insert into {}(bikeId,distX,distY)
        values(%s,%s,%s);
    '''

dataName=['guanggu71308','guanggu71309','guanggu71310','guanggu71312','guanggu71313',
'guanggu71314','guanggu71315','guanggu71317','guanggu71318','guanggu71319','guanggu71320',
'guanggu71321','guanggu71322']
def DataDelete(sqlName):
    sql=sql1.format(sqlName)
    conn=pymysql.Connect(host='localhost', user='root',password='123456',db='mobike',charset='utf8')
    cur=conn.cursor()
    cur.execute(sql)
    result=cur.fetchall()
    data1=np.array(result)
    data1[:,1:3]=data1[:,1:3].astype('float64')[:,:]
    i=0
    cleanDataID=list()
    cleanData=list()
    for item in data1:
        if item[0] not in cleanDataID:
            cleanDataID.append(item[0])
            data1[i][0]=item[0][0:-1]
            #print(item[0],cleanDataID)
            cleanData.append(data1[i])
        i=i+1
    cur.close()
    conn.close()
    return cleanData

def SaveCleanData(cleanData,sqlName):
    Csql3=sql3.format(sqlName)
    Csql4=sql4.format(sqlName)
    conn=pymysql.connect(host='localhost',user='root',password='123456',db='processedmobike',charset='utf8')
    cur=conn.cursor()
    cur.execute(Csql3)
    for item in cleanData:
        cur.execute(Csql4,(str(item[0]),str(item[1]),str(item[2])))
    conn.commit()
    cur.close()
    conn.close()

def run():
    for item in dataName:
        cleanData=DataDelete(item)
        SaveCleanData(cleanData,item)

run()