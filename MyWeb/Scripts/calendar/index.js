
$(function () {

    config.url = "/Calendar/";
    own.loading.show();
    own.ajax.post("GetData1", {}, function (res) {
        own.loading.hide();
        $(".tableBox").append(res);
    });
    $(document).off().on("click", ".calendar", function () {
        var obj = $(this);
        var year = obj.data("year");
        var semeter = obj.data("semeter");
        var month = obj.data("month");
        var week = obj.data("week");
        var str = "请输入" + month + "月第" + week + "周的教学计划";        layer.open({
            type: 1,
            title: "教师教学计划",
            area: ["450px", "300px"],
            move: false,
            content: '<div class="ls-box"><textarea id="ly-textarea" placeholder="' + str + '">' + obj.attr("data-html") + '</textarea>' +
            '<div class="btn-list-box"><a class="layui-btn layui-btn-normal layui-btn-sm" id="save-btn">保存</a>&nbsp;&nbsp;&nbsp;&nbsp;<a class="layui-btn  layui-btn-primary layui-btn-sm" id="close-btn">关闭</a></div>' +
            '</div>'

        });        $("#save-btn").off().on("click", function () {
            var txt = $("#ly-textarea").val();
            if (txt.length == 0) {
                _Savedata("");
            }
            else {
                if (txt.length < 10 || txt.length > 1200) {
                    $layer.alert("提示", "教学计划文字最少10个字最多1200字", 0); return;
                }
                else {
                    _Savedata(txt);
                }
            }
        })

        $("#close-btn").off().on("click", function () { //取消
            $layer.close();
        });        function _Savedata(txt) {
            var data = {
                year: year,
                month: month,
                week: week,
                semeter: semeter,
                content: txt
            }
            console.log(data)
            own.ajax.post("Calendar", data, function (out) {
                if (out.code == 200) {
                    $layer.close();
                    layer.msg('信息更新成功!');
                    obj.html(subStr(txt, 100).replace(new RegExp(/\n/g), '<br>')).attr("data-html", txt);

                    function subStr(str, len) {
                        var s = "";
                        if (str.length <= len) s = str;
                        else {
                            s = str.substring(0, len) + "\n ...";
                        }
                        return s;
                    }

                } else {
                    layer.msg('信息更新失败!');
                }
            }, "json")
        }
    })    //切换学年
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
        $(".tableBox").html("");
        own.loading.show();

        var data = {
            year: year,
            semester: semester
        };
        own.ajax.post("GetData1", data, function (res) {
            own.loading.hide();
            $(".tableBox").append(res);
        });
    }})
