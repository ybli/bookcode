

function login(){
    var username = document.getElementById("ID").value;
    var password = document.getElementById("PASSWORD").value;
    if(username==""){
        $.jGrowl("用户名不能为空！", { header: '提醒' });
    }else if(password==""){
        $.jGrowl("密码不能为空！", { header: '提醒' });
    }else{
        AjaxFunc();
    }
}
function submit(){
    var username = document.getElementById("ID").value;
    var password = document.getElementById("PASSWORD").value;
    var password2 = document.getElementById("PASSWORD2").value;
    if(username==""){
        $.jGrowl("用户名不能为空！", { header: '提醒' });
    }else if(password==""){
        $.jGrowl("密码不能为空！", { header: '提醒' });
    }else if(password!=password2){
        $.jGrowl("两次密码输入不一致", { header: '提醒' });
    }else{
        AjaxFuncsub();
    }

    // alert("注册成功");
    // window.location.href="login.html";
}

//register=function(){
//    alert("1!");
//    window.location.href="/index/";
//}
function AjaxFunc()
{
    var username = document.getElementById("ID").value;
    var password = document.getElementById("PASSWORD").value;
    $.ajax({
        type: 'GET',
        url: "http://127.0.0.1:8000/check/",
        dataType: "json",
        data: {"user": username,"password": password},
        success: function (data) {
            console.log(data.username);
            console.log(password);
            if(data=='1')
            {
                window.location.href="/index/";
            }
            else{
                $.jGrowl("用户名或密码填写错误！", { header: '提醒' });
            }
        },
        error: function (xhr, type) {
            console.log(xhr);
        }
    });
}

function AjaxFuncsub()
{
    var username = document.getElementById("ID").value;
    var password = document.getElementById("PASSWORD").value;
    $.ajax({
        type: 'GET',
        url: "http://127.0.0.1:8000/handle/register/",
        dataType: "json",
        data: {"user": username,"password": password},
        success: function (data) {
            console.log(data.username);
            console.log(password);
            if(data=='1')
            {
                alert("注册成功")
                window.location.href="/login/";
            }
            else{
                $.jGrowl("信息填写有误", { header: '提醒' });
            }
        },
        error: function (xhr, type) {
            console.log(xhr);
        }
    });
}
