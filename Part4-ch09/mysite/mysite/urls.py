"""mysite URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/2.1/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  path('', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  path('', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.urls import include, path
    2. Add a URL to urlpatterns:  path('blog/', include('blog.urls'))
"""
from django.contrib import admin
from django.urls import path,include
from cmdb import views as cmdbviews

urlpatterns = [
    path('admin/', admin.site.urls),
    path('login/',cmdbviews.login),
    path('check/',cmdbviews.check),
    path('handle/',include('cmdb.urls')),
    path('heatmap/',cmdbviews.heatmap),
    path('index/',cmdbviews.index),
    path('menu/',cmdbviews.Menu),
    path('line/',cmdbviews.line),
    path('movemap/',cmdbviews.MoveMap),
    path('spider/',cmdbviews.SpiderHandle),
    path('list/',cmdbviews.list),
    path('radar/',cmdbviews.radar),
    path('meituan/',cmdbviews.MeituanA),
    path('zonghe/',cmdbviews.Zonghe),
    path('point/',cmdbviews.Point),
    path('threeD/',cmdbviews.ThreeD),
    path('register/',cmdbviews.reg),
    path('zombie/',cmdbviews.zombie),
    path('route/',cmdbviews.Route),
    path('record/',cmdbviews.Record),
    path('report/',cmdbviews.Report)

]
