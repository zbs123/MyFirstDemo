//layUI
//导航--依赖 element 模块，否则无法进行功能性操作
layui.use('element', function () {
    var element = layui.element;
});

window.onload = function () {
    //左侧导航点击
    var listIndex;
    $(".layui-nav-item>a").click(function () {
        listIndex = $(this).text();
        if (listIndex == "工资条") {
            $("#rightBox").attr("src", "pay");
        } else if (listIndex == "个人工资条") {
            $("#rightBox").attr("src", "payRoll");
        }

    });
}

