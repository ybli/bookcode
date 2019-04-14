from django.urls import path
from . import views
urlpatterns=[
    path('GetCenter/',views.GetCenter),
    path('StaticMap/',views.StaticMap),
    path('DynamicMap/',views.DynamicMap),
    path('Spider/',views.Spider),
    path('Buffer/',views.Buffer),
    path('Meituan/',views.Meituan),
    path('MeituanPoint/',views.MeituanPoint),
    path('AllDay/',views.AllDay),
    path('Register/',views.register),
    path('UserPoint/',views.UserPoint),
    path('GetZombie/',views.GetZombie),
    path('GetRoute/',views.GetRoute),
    path('UserData/',views.GiveData)
]