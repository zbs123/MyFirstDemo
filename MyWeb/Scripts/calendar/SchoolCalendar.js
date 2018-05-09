$(function () {
    config.url = "/Calendar/";
    var type_color = {
        "0": "#333",
        "1": "#f58828",
        "2": "#ff4848",
        "3": "#357be8",
        "4": "#d9d9d9"
    }
    var type_bgcolor = {
        "0": "",
        "1": "rgb(254, 244, 230)",
        "2": "rgba(208, 1, 27, 0.1)",
        "3": "rgba(36,105,244,.1)",
        "4": ""
    }
    var start, end;
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth();
    var days = date.getDay();
    var semester = "2";
    var sarr = [];
    if (month + 1 > 8 || month + 1 == 1) {
        semester = "1";
    }
    if (semester == "1") {
        sarr = [9, 10, 11, 12, 1];
    } else {
        sarr = [3, 4, 5, 6, 7];
    }
    //生成表格
    function GetTableStr() {
        var roletype = $("#roletype").val();
        var dateclass = "", descc = "";
        if (roletype >= 8) {
            dateclass = "datetext";
            descc="desc-type"
        }
        var theadStr = "";
        theadStr += "<tr><td>月份</td><td>周次</td><td>周一</td><td>周二</td><td>周三</td><td>周四</td><td>周五</td><td>周六</td><td>周日</td><td>校历说明</td></tr>";
        $("#calendar thead").html("")
        $("#calendar thead").append(theadStr);
        var date_count = getMonDate(year, sarr[0] - 1).getDate();
        var date_week = 1;
        var tbodyStr_my = "";
        var k = 0;//每7个换行
        var m_l_row = 0;//记录月份共有几行
        var flag = false;
        var tbodyStr = "";

        for (var i = 0; i < sarr.length; i++) {
            for (var j = date_count; j <= mGetDate(year, sarr[i]); j++) {
                if (k % 7 == 0) {
                    if (flag) {

                        var index = tbodyStr.indexOf("<tr>", 2);
                        tbodyStr = tbodyStr.substring(0, index) + "<td rowspan='" + m_l_row + "' class='edit-desc " + descc + "'  data-month='" + sarr[i - 1] + "'><div class='desc-text-wrap'><span>点击输入校历说明</span></div><div class='desc-shade'><i class='layui-icon'>&#xe642;</i>编辑</div><div class='desc-textarea layui-hide'><div class='desc-textarea-body-wrap'><textarea placeholder='请输入校历说明...'></textarea></div><div class='desc-textarea-footer-wrap'><div class='desc-textarea-footer'><button class='btn btn-important block ' type='button'>保存</button></div></div></div></td></tr>" + tbodyStr.substring(index, tbodyStr.length);
                        tbodyStr = "<tr><td class='date-month' rowspan='" + m_l_row + "'>" + sarr[i - 1] + "月</td>" + tbodyStr.substring(4, tbodyStr.length);
                        m_l_row = 0;
                        tbodyStr_my += tbodyStr;
                        tbodyStr = "";
                        flag = false;
                    }
                    tbodyStr += "<tr><td class='date-week'>第" + date_week + "周</td>";
                    date_week++;
                    m_l_row++;
                }
                if ((k + 1) % 7 == 0 || (k - 5) % 7 == 0) {
                    tbodyStr += "<td class='" + dateclass+" ' data-day='" + date_count + "' data-month='" + sarr[i] + "' style='color:" + type_color[4] + "'><div class='date-content'><span>" + date_count + "</span><span ></span></div></td>";
                } else {
                    tbodyStr += "<td class='" + dateclass +"  ' data-day='" + date_count + "' data-month='" + sarr[i] + "'><div class='date-content'><span>" + date_count + "</span><span ></span></div></td>";
                }

                date_count++;
                k++;
            }
            date_count = 1;
            flag = true;
        }
        var index = tbodyStr.indexOf("<tr>", 2);
        tbodyStr = tbodyStr.substring(0, index) + "<td rowspan='" + m_l_row + "' class='edit-desc " + descc + "'  data-month='" + sarr[i - 1] +"'><div class='desc-text-wrap'><span>点击输入校历说明</span></div><div class='desc-shade'><i class='layui-icon'>&#xe642;</i>编辑</div><div class='desc-textarea layui-hide'><div class='desc-textarea-body-wrap'><textarea placeholder='请输入校历说明...'></textarea></div><div class='desc-textarea-footer-wrap'><div class='desc-textarea-footer'><button class='btn btn-important block ' type='button'>保存</button></div></div></div></td></tr>" + tbodyStr.substring(index, tbodyStr.length);
        tbodyStr = "<tr><td class='date-month' rowspan='" + m_l_row + "'>" + sarr[i - 1] + "月</td>" + tbodyStr.substring(4, tbodyStr.length);
        tbodyStr_my += tbodyStr;
        if (k < (date_week - 1) * 7) {
            for (var i = 0; i < (date_week - 1) * 7 - k; i++) {
                tbodyStr_my += "<td style='color:" + type_color[4] + "'>" + (i + 1) + "</td>"
            }
        }
        tbodyStr_my += "</tr>";
        $("#calendar tbody").html("");
        $("#calendar tbody").append(tbodyStr_my);

        //遮罩
        $(".desc-type").hover(
            function () {
                $(this).find(".desc-shade").css("visibility", "visible").css("opacity", "1");
            },
            function () {
                $(this).find(".desc-shade").css("visibility", "hidden").css("opacity", "0");
            }
        )
    }
    function getMonDate(year, month) {
        var d = new Date(year, month),
            day = d.getDay(),
            date = d.getDate();
        if (day == 1)
            return d;
        if (day == 0)
            d.setDate(date + 1);
        else
            d.setDate(date + 7 - day + 1);
        return d;
    }
    //获取当月天数
    function mGetDate(year, month) {
        var d = new Date(year, month, 0);
        return d.getDate();
    }
    GetTableStr();
    GetTableData();
    //加载表格数据
    function GetTableData() {
        
        own.loading.show();
        own.ajax.post("GetSchoolCalendar", {
            jw_schoolyear: year
            , jw_semester: semester
        }, function (res) {
            own.loading.hide();
            var dataArr = res.data;
            console.log(dataArr)
            for (var i = 0; i < dataArr.length; i++) {
                if (dataArr[i].jw_day) {
                    var d_html = $("#calendar tbody").find("[data-month='" + dataArr[i].jw_month + "'][data-day='" + dataArr[i].jw_day + "']");
                    $(d_html).attr("data-contentType", dataArr[i].jw_contentType);
                    $(d_html).attr("data-note", dataArr[i].jw_note);
                    $(d_html).attr("data-startTime", dataArr[i].strStartTime);
                    $(d_html).attr("data-endTime", dataArr[i].strEndTime);
                    $(d_html).css("color", type_color[dataArr[i].jw_contentType]);//改变颜色
                    $(d_html).css("background-color", type_bgcolor[dataArr[i].jw_contentType]);//改变颜色
                    $(d_html).find("span:last").text(dataArr[i].jw_note);//备注赋值
                    if (dataArr[i].jw_contentType == 2 || dataArr[i].jw_contentType == 3) {
                        var day1 = new Date(dataArr[i].strStartTime);
                        var day2 = new Date(dataArr[i].strEndTime);
                        var day_sum = parseInt((day2 - day1) / (1000 * 60 * 60 * 24));//天数

                        for (var t = 0; t < day_sum; t++) {
                            day1.setDate(day1.getDate() + 1);
                            var d_html_a;

                            d_html_a = $("#calendar tbody").find("[data-month='" + (day1.getMonth() + 1) + "'][data-day='" + day1.getDate() + "']");
                            $(d_html_a).attr("data-month", dataArr[i].jw_month);
                            $(d_html_a).attr("data-day", dataArr[i].jw_day);

                            $(d_html_a).attr("data-contentType", dataArr[i].jw_contentType);
                            $(d_html_a).attr("data-note", dataArr[i].jw_note);
                            $(d_html_a).attr("data-startTime", dataArr[i].strStartTime);
                            $(d_html_a).attr("data-endTime", dataArr[i].strEndTime);
                            $(d_html_a).css("color", type_color[dataArr[i].jw_contentType]);//改变颜色
                            $(d_html_a).css("background-color", type_bgcolor[dataArr[i].jw_contentType]);//改变颜色
                        }
                    }
                } else {
                    if (dataArr[i].jw_contentDesc) {
                        $(".edit-desc[data-month='" + dataArr[i].jw_month + "'] div span").text(dataArr[i].jw_contentDesc);
                    } else {
                        $(".edit-desc[data-month='" + dataArr[i].jw_month + "'] div span").text("点击输入校历说明");
                    }
                }
                
            }

        }, "json");
    }

    //单元格点击
    $(document).on("click", ".datetext", function () {
    //$(".datetext").off().on("click", function () {
        $(".date-content").removeClass("my-seleted");
        $(".add-event-wrap").removeClass("layui-hide");

        var obj = $(this);
        obj.find(".date-content").addClass("my-seleted");
        //设置弹出框位置
        var x = obj.offset().left + obj.width() + 5;
        if (x + $(".add-event-wrap").width() + 30 >= $(window).width()) {
            x = obj.offset().left - $(".add-event-wrap").width()-30;
        }
        var y = obj.offset().top;
        var h = $(document).height() - y ;
        $(".add-event-wrap").attr("style", "transform:translate(" + x + "px,-" + h + "px);");
        //设置时间空间
        var contentType = obj.attr("data-contenttype");
        $(".event-input").val(obj.attr("data-note"));
       
        let stt = obj.attr("data-starttime");
        let edt = obj.attr("data-endtime");
        if (stt) {
            $("#test1").val(stt);
        } else {
            let now = year + "-" + obj.attr("data-month") + "-" + obj.attr("data-day")
            $("#test1").val(now);
        }
        if (edt) {
            $("#test2").val(edt);
        } else {
            let now = year+"-"+obj.attr("data-month") +"-"+obj.attr("data-day")
            $("#test2").val(now);
        }
        if (contentType) {
            SetDateShow(contentType);
            $(".contentType").val(contentType);
        } else {
            SetDateShow(0);
            $(".contentType").val(0);
        }
        layui.form.render('select');
    })
    //关闭弹框
    $(".ae-close").off().on("click", function () {
        close();

    })

    //保存按钮
    $(".save").off().on("click", function () {
        var n_m = "",n_d="";//放假和特殊新的月日
        var m = $(".my-seleted").parent().data("month");
        var d = $(".my-seleted").parent().data("day");
        var contentType = $(".contentType").val();
        var note = $(".event-input").val();
        var startTime = "", endTime = "";
        if (note.length > 8) {
            layer.msg("备注不能超过8个字！"); return;
        }
        if (contentType == "1") {
            startTime = $("#test1").val();
            if (new Date(startTime).toLocaleDateString() == new Date(year, m - 1, d).toLocaleDateString()) {
                layer.msg("当前日期不能与原上课日期相同");
                return;
            }
            note = "补" + new Date(startTime).getDate() + "日课";
        }
        if (contentType == "2" || contentType == "3") {
            startTime = $("#test1").val();
            endTime = $("#test2").val();
            let day1 = new Date(startTime);
            n_m = day1.getMonth()+1;
            n_d = day1.getDate();
            let day2 = new Date(endTime);
            if (day2 < day1) {
                layer.msg("结束日期不能大于开始日期");
                return;
            }
            let day_sum = parseInt((day2 - day1) / (1000 * 60 * 60 * 24));//天数
            let l_msg = "";
            let l_html = "<div class='msg-box'><ol>";
            for (var t = 0; t < day_sum; t++) {
                day1.setDate(day1.getDate() + 1);
                let d_html_a;

                d_html_a = $("#calendar tbody").find("[data-month='" + (day1.getMonth() + 1) + "'][data-day='" + day1.getDate() + "']");
                if ($(d_html_a).attr("data-contentType")>0 || $(d_html_a).attr("data-note")) {
                    l_msg +="<li>"+ day1.toLocaleDateString()+ "已存在其他状态，请先将其设置为正常并清空备注"+"</li>";
                }
            }
            if (l_msg.length > 0) {
                l_html += l_msg + "</ol></div><div class='msg-box-btn'> <button class='layui-btn layui-btn-normal'>知道了</button></div>";
                layer.open({
                    type: 1,
                    title: '状态冲突',
                    closeBtn: 0,
                    shadeClose: false,
                    skin: '',
                    area:'420px',
                    content: l_html
                });
                $(".msg-box-btn button").off().on("click", function () {
                    layer.closeAll();
                })
                return;
            }
            
        }
        var data = {
            jw_schoolyear: year
            , jw_semester: semester
            , jw_month: m
            , jw_day: d
            , jw_note: note
            , jw_contentType: contentType
            , jw_startTime: startTime
            , jw_endTime: endTime
            , new_m: n_m
            , new_d: n_d
        }
        own.ajax.post("AddSchoolCalendar", data, function (res) {
            if (res.code == "200") {
                layer.msg("添加成功")
                //let old_m_d = $("#calendar tbody").find("[data-month='" + m + "'][data-day='" + d + "']");
                //for (var i = 1; i < old_m_d.length; i++) {
                //    $(old_m_d[i]).attr("data-month", n_m).attr("data-day", n_d);
                //}
                GetTableStr();
                GetTableData();
                close();
            } else {
                layer.msg("添加失败")
            }
        }, "json");
    })
    //编辑按钮
    $(document).on("click", ".desc-shade", function () {

    //$(".desc-shade").off().on("click", function () {
        $(".desc-textarea").addClass("layui-hide");
        var obj = $(this);
        
        obj.next(".desc-textarea").removeClass("layui-hide");
        let now_desc = obj.prev().text();
        if (now_desc != "点击输入校历说明") {
            obj.next().find("textarea").val(now_desc)
        }
    })
    //校历说明添加
    $(document).on("click", ".desc-textarea button", function () {

    //$(".desc-textarea button").off().on("click", function () {
        var m = $(this).parents("td").data("month");
        var desc = $(this).parent().parent().parent().find("textarea").val();
        var data = {
            jw_schoolyear: year
            , jw_semester: semester
            , jw_month: m
            , jw_contentdesc: desc
        }
        own.ajax.post("AddSchoolCalendarDesc", data, function (res) {
            if (res.code == "200") {
                layer.msg("添加成功")
                GetTableData();
                $(".desc-textarea").addClass("layui-hide");
            } else {
                layer.msg("添加失败")
            }
        }, "json");
    })
   
    function close() {
        var form = layui.form;
        $(".event-input").val("");
        $(".contentType").val("0");
        form.render('select');
        $(".date-content").removeClass("my-seleted");
        $(".add-event-wrap").addClass("layui-hide");
        $("#test1").parent().parent().addClass("layui-hide");
        $("#test2").parent().parent().addClass("layui-hide");
    }
    function SetDateShow(v) {
        if (v == "0") {
            $("#test1").parent().prev().text("开始日期");
            $("#test1").parent().parent().addClass("layui-hide");
            $("#test2").parent().parent().addClass("layui-hide");
        } else if (v == "1") {
            start.config.min = "";
            end.config.min = "";
            $("#test1").parent().prev().text("补课日期");
            $("#test1").parent().parent().removeClass("layui-hide");
            $("#test2").parent().parent().addClass("layui-hide");
        } else {
            let n = new Date($("#test1").val())
            //开始时间从当前时间开始选择
            start.config.min = {
                year: year,
                month: n.getMonth(),
                date: n.getDate()
            }
            end.config.min = {
                year: year,
                month: n.getMonth(),
                date: n.getDate()
            }
            $("#test1").parent().prev().text("开始日期");
            $("#test1").parent().parent().removeClass("layui-hide");
            $("#test2").parent().parent().removeClass("layui-hide");
        }
    }
    layui.use('laydate', function () {
        var laydate = layui.laydate, form = layui.form;

        //执行一个laydate实例
        start=laydate.render({
            elem: '#test1' //指定元素
            , calendar: true
            , value: new Date()
           
        });
       end= laydate.render({
            elem: '#test2' //指定元素
            , calendar: true
            , value: new Date()
        });
       form.on('select', function (data) {
            var v = data.value; //得到被选中的值
            SetDateShow(v)
        });
    });
    $("#export").off().on("click", function (e) {
        var obj = $(this);
        own.loading.show();
        obj.addClass("layui-hide");
        $(".date-content").removeClass("my-seleted");
        $(".add-event-wrap").addClass("layui-hide");
        html2canvas(document.querySelector(".container"), { height: $(document).height() }).then(canvas => {
            //document.body.appendChild(canvas)
            obj.removeClass("layui-hide");
            var image = canvas.toDataURL("image/png");
            own.ajax.post("ExportImg", { imgData: image }, function (res) {
                own.loading.hide();
                if (res.code == "200") {
                    $(".export-save").attr("href", "/calendar/open1?url=" + res.data);
                    $("#sp").trigger("click");
                } else {
                    layer.msg("出错了哦");
                }
                
            }, "json");
        });
        
    })
})
