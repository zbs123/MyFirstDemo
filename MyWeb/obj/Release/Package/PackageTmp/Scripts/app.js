$(function () {
   /* $(window).scroll(function () {
        if ($(document).scrollTop() > 300) {
            $("#returnTop").show();
        }
        else $("#returnTop").hide();
    })
    $("#returnTop").click(function () {
        var speed = 200;//滑动的速度
        $('body,html').animate({ scrollTop: 0 }, 500);
        return false;
    });*/

    layui.use('util', function () {
        var util = layui.util;

        //执行
        util.fixbar({
            bar1: false
            , click: function (type) {
                console.log(type);

            }
            , css: { bottom: 100 }
            , showHeight: 30
        });
    });

    own.loading.hide();
})

function LoadLayUi() {
    
    layui.use('form', function () {
        var form = layui.form;
        form.render('select');
        //各种基于事件的操作，下面会有进一步介绍
    });
}
/**
 * iframe 打开页面
@param title 页面的标题
@param w 宽度 ； 可使用百分比
@param h 高度
@param url 地址
 */
function iframe(title,w,h ,url,top) {
    layer.open({
        type: 2,
        title: title,
        shadeClose: true,
        move: false,  //禁止移动
        shade: 0.4,
        area: [w, h],
        content: url//iframe的url
    }); 


    $(".layui-layer-iframe").css({"top": top+"px"}) /**为了兼容火狐浏览器**/

}
var $layer = {
    alert: function (title, con, icon) {
        layer.alert(con, {
            icon: icon, //0警告，1 成功，2失败，3疑问，4 锁定，5哭脸，6笑脸
            title: title
        })
    },
    alertCall: function (str, call) { //弹出框，点击按钮回掉函数
        layer.confirm(str, {
            btn: ['确定'] //按钮
        }, function () {
            if (typeof call === "function") {
                call();
            }
        });
    },
    confirm: function (msg, call) {//询问框
        layer.confirm(msg, {
            btn: ['确定', '取消'] //按钮
        }, function () {
            if (typeof call === "function") {
                call();
            }
        }, function () {

        });

        $(".layui-layer-dialog").css({ "top": "30%" });

    },
    close: function () {
        // layer.close($(".layui-layer").attr("times"));
        layer.closeAll();
    }
}
