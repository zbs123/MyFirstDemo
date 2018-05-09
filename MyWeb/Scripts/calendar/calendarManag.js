$(function () {
    var flag=true;//判断分页是否第一次加载
    var page = 1;
    var limit = 17;
    own.loading.show();
    config.url = "/Calendar/";
    GetHead("","",page,limit);
    function GetHead(year,semester) {
        own.ajax.post("GetHead", {year:year,semester:semester}, function (res) {
            own.loading.hide();
            $("#semester").val(res.semeter);
            //console.log(res);
            if (res.code == "200") {
                var headStr = "<tr>";
                headStr += " <th rowspan='2'>编号</th><th rowspan= '2' > 姓名</th >";
                for (var i = 0; i < res.data.length; i++) {
                    headStr += "<th colspan='4'>" + res.year + "年/" + res.data[i] + "月</th>";
                }
                headStr += "</tr><tr>";
                for (var i = 0; i < 5; i++) {
                    headStr += "<th>1</th><th>2</th><th>3</th><th>4</th>"
                }
                headStr += "</tr>";
                $("#calendar thead").append(headStr);
                GetBody(res.year,res.semeter,page,limit)
            }
        }, "json");
    }
   

    function GetBody(year, semester, page, limit,search) {
        own.loading.show();
        own.ajax.post("GetData", { year: year, semester: semester, page: page, limit: limit, search: search }, function (out) {
            own.loading.hide();
            //console.log(out);
            
            var myout = out.data;
            if (out.code == "200") {
                let tbodyStr = "";
                for (var i = 0; i < myout.length; i++) {
                    tbodyStr += "<tr>";
                    tbodyStr += "<td>" + myout[i].ri_id + "</td><td>" + myout[i].ri_realname + "</td>";
                    for (var j = 0; j < myout[i].cmlist.length; j++) {
                        let jcv = myout[i].cmlist[j].jw_content_view;
                        if (jcv == "已写") {
                            tbodyStr += "<td class='jcv_content' data-content='" + myout[i].cmlist[j].jw_content + "'>" + jcv + "</td>";
                        } else {
                            tbodyStr += "<td>" + jcv + "</td>";
                        }

                    }
                    tbodyStr += "</tr>";
                }
                $("#calendar tbody").html("");
                $("#calendar tbody").append(tbodyStr);
                if (flag) {
                    layui.use('laypage', function () {
                        var laypage = layui.laypage;
                        //执行一个laypage实例
                        laypage.render({
                            elem: 'mypage' //注意，这里的 test1 是 ID，不用加 # 号
                            , count: out.count //数据总数，从服务端得到
                            , limit: 20
                            , jump: function (obj, first) {
                                currentPageAllAppoint = obj.curr;
                                //首次不执行
                                if (!first) {
                                    GetBody("", "", obj.curr, limit, $("#search").val());
                                }
                            }
                        });
                    });
                }
                flag = false;
            } else {

            }
        }, "json");
    }
    $(document).on("click", ".jcv_content", function () {
        layer.tips($(this).data("content"), $(this), {
            tips: [3, '#0FA6D8'] //还可配置颜色
        });
    })
    //切换学年
    $(".curr").off().on("click", function () {
        var o = $(".year-box");
        var obj = $(this);
        if (o.css("display") == "none") {
            o.fadeIn(200);
            o.find("li").off().on("click", function () {

                obj.html($(this).html()).attr("data-v", $(this).data("v"));
                o.fadeOut(200);

                year = $(this).data("v");
                semester = 1;
                switchover(year, semester);

            })
        }
        else o.fadeOut(200)
    });

    //上学期下学期切换按钮
    $(".sem-btn").off().on("click", function () {
        year = $(".curr").attr("data-v");
        semester = $(this).data("s");
        switchover(year, semester);
    })    function switchover(year, semester) {
        
        own.loading.show();
        $("#search").val("");
        flag = true;
        $("#calendar thead").html("");
        $("#calendar tbody").html("");
        GetHead(year, semester);
    }    //搜索
    $(".searchBox i").off().on("click", function () {
        var search = $("#search").val();
        year = $(".curr").attr("data-v");
        $("#calendar tbody").html("");
        flag = true;
        GetBody($(".curr").attr("data-v"), $("#semester").val(), page, limit, search);
    })
})
