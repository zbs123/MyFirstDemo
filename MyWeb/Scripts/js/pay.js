layui.use(['table','upload'], function () {
    var table = layui.table
        , upload = layui.upload;
    own.loading.show();
    table.render({
        elem: '#payRollTab'
        , url: 'GetData'
        //, data: res.data
        , limit: 100
        , cols: [[
            { type: 'checkbox' }
            , { field: 'xw_Id', width: 140, title: '序号' }
            , { field: 'xw_Username', width: 140, title: '姓名' }
            , { field: 'Month', width: 140, title: ' 时间' }
            , { field: 'xw_PostSalary', width: 140, title: '岗位工资' }
            , { field: 'xw_PayWages', width: 140, title: '薪级工资' }
            , { field: 'xw_ApprenticeWages', title: '见习工资', minWidth: 100 }
            , { field: 'xw_TenWage', width: 140, title: '10%工资' }
            , { field: 'xw_BasicAllowance', width: 150, title: '基础绩效岗位津贴' }
            , { field: 'xw_AgeAllowance', width: 140, title: '教龄津贴' }
            , { field: 'xw_STAllowance', width: 140, title: '特级教师津贴' }
            , { field: 'xw_Subsidies', width: 140, title: '116补贴' }
            , { field: 'xw_TownSubsidies', width: 140, title: '乡镇工作补贴' }
            , { field: 'xw_BreadStick', width: 140, title: '粮贴' }
            , { field: 'xw_OnlyFee', width: 140, title: '独子费' }
            , { field: 'xw_ShouldSend', title: '应发合计', minWidth: 100 }
            , { field: 'xw_IncomeTax', width: 140, title: '所得税' }
            , { field: 'xw_AssDues', width: 140, title: '公会会费' }
            , { field: 'xw_HouseFee', width: 140, title: '住房公积金' }
            , { field: 'xw_UnemployIn', width: 140, title: ' 失业保险' }
            , { field: 'xw_MedicalIn', width: 140, title: ' 医疗保险' }
            , { field: 'xw_EndIn', width: 140, title: ' 养老保险' }
            , { field: 'xw_PerPension', width: 140, title: ' 个人职业年金' }
            , { field: 'xw_OtherDedu', width: 140, title: ' 其他扣款' }
            , { field: 'xw_Detain', width: 140, title: ' 扣发合计' }
            , { field: 'xw_RealWages', width: 140, title: ' 实发工资' }
        ]]
        , done: function (res) {
            own.loading.hide();
            if (res.count == 0) {
                $("#noData").removeClass("layui-hide");
                $(".layui-none").remove();
                $(".layui-table-page").remove();
            } else {

                $("#noData").addClass("layui-hide");
            }
        }
    });
    //own.ajax.post("GetData", {}, function (res) {
    //    if (res.code == "0") {
    //        //表格
            
    //    }
    //}, "json")
    
    //监听表格复选框选择
    table.on('checkbox(tableTest)', function (obj) {
        //console.log(obj)
    });
    

    //导入
    var uploadInst = upload.render({
        elem: ".inport"
        , url: "inport"
        , accept: 'file' //普通文件
        , exts: 'xlsx|xls' //只允许上传压缩文件
        , data: {
            month: "2018-3"
        }
        , before: function (obj) { //obj参数包含的信息，跟 choose回调完全一致，可参见上文。
            jQuery(".chooseFile").css("display", "none");
            jQuery(".chooseLoad").css("display", "block");
        }
        , done: function (res) {
            jQuery(".chooseLoad").css("display", "none");
            jQuery(".chooseTable").css("display", "block");
            SetTable(res.data);
            
        }
        , error: function () {
            jQuery(".chooseFile").css("display", "block");
            jQuery(".chooseLoad").css("display", "none");
            jQuery(".chooseTable").css("display", "none");
        }
    })


    function SetTable(data) {
        var table = layui.table;
        table.render({
            elem: '#chooseTab'
            , data: data
            , width: 718
            , height: 335
            ,limit:data.length+1
            , cols: [[
                { type: 'numbers' }
                , { field: 'xw_Username', width: 140, title: '姓名' }
                , { field: 'xw_PostSalary', width: 140, title: '岗位工资', edit: 'text' }
                , { field: 'xw_PayWages', width: 140, title: '薪级工资', edit: 'text' }
                , { field: 'xw_ApprenticeWages', title: '见习工资', minWidth: 100, edit: 'text'}
                , { field: 'xw_TenWage', width: 140, title: '10%工资', edit: 'text'}
                , { field: 'xw_BasicAllowance', width: 150, title: '基础绩效岗位津贴', edit: 'text' }
                , { field: 'xw_AgeAllowance', width: 140, title: '教龄津贴', edit: 'text'}
                , { field: 'xw_STAllowance', width: 140, title: '特级教师津贴', edit: 'text' }
                , { field: 'xw_Subsidies', width: 140, title: '116补贴', edit: 'text'}
                , { field: 'xw_TownSubsidies', width: 140, title: '乡镇工作补贴', edit: 'text' }
                , { field: 'xw_BreadStick', width: 140, title: '粮贴', edit: 'text'}
                , { field: 'xw_OnlyFee', width: 140, title: '独子费', edit: 'text' }
                , { field: 'xw_ShouldSend', title: '应发合计', minWidth: 100}
                , { field: 'xw_IncomeTax', width: 140, title: '所得税', edit: 'text' }
                , { field: 'xw_AssDues', width: 140, title: '公会会费', edit: 'text'}
                , { field: 'xw_HouseFee', width: 140, title: '住房公积金', edit: 'text'}
                , { field: 'xw_UnemployIn', width: 140, title: ' 失业保险', edit: 'text' }
                , { field: 'xw_MedicalIn', width: 140, title: ' 医疗保险', edit: 'text' }
                , { field: 'xw_EndIn', width: 140, title: ' 养老保险', edit: 'text'}
                , { field: 'xw_PerPension', width: 140, title: ' 个人职业年金', edit: 'text'}
                , { field: 'xw_OtherDedu', width: 140, title: ' 其他扣款', edit: 'text' }
                , { field: 'xw_Detain', width: 140, title: ' 扣发合计' }
                , { field: 'xw_RealWages', width: 140, title: ' 实发工资' }
            ]]
        });
        //监听单元格编辑
        table.on('edit(chooseTab)', function (obj) {
            console.log(obj)
            var value = obj.value //得到修改后的值
                , data = obj.data //得到所在行所有键值
                , field = obj.field; //得到字段
            //console.log(jQuery(this).parent().siblings().find("[data-field='xw_ShouldSend']"));
            var jia = 0;
            var jian = 0;
            jQuery(this).parent().parent().find("td").each(function (index, item) {
                if ($(this).data("field") == obj.field) {
                    if (index < 13) {
                        data.xw_ShouldSend = parseFloat(data.xw_ShouldSend) + (parseFloat(value)-parseFloat($(this).text()));
                        jia = data.xw_ShouldSend;
                    }
                    if (index > 13) {
                        data.xw_Detain = parseFloat(data.xw_Detain) + (parseFloat(value)-parseFloat($(this).text()));
                        jian = data.xw_Detain;
                    }
                }
                if (index == 13 && jia != 0) {
                    $(this).text(jia)
                }
                if (index == 22 && jian != 0) {
                    $(this).text(jian);
                }
                if (index == 23) {
                    data.xw_RealWages = data.xw_ShouldSend - data.xw_Detain;
                    $(this).text(data.xw_ShouldSend - data.xw_Detain);
                }
            })
            
            //layer.msg('[ID: ' + data.id + '] ' + field + ' 字段更改为：' + value);
        });
        
        
    }
});
$(function () {
    //导入
    $("#leadIn").click(function () {
        var table = layui.table;
        layer.open({
            type: 1,
            title: "批量导入",
            area: ['750px', '500px'], //宽高
            content: $(".leadinBox"),
            btn: ['确定', '取消'], //按钮
            yes: function (index) {
                console.log(table.cache.chooseTab)
                own.ajax.post("InportDb", {
                    dataArr: JSON.stringify(table.cache.chooseTab)
                }, function (res) {
                    if (res.code == "200") {
                        table.reload('payRollTab', {
                            url: 'GetData'
                            , where: {
                                uname: jQuery("#uname").val(),
                                month: jQuery("#month").val()
                            }
                        });
                    }
                    else if (res.code == "20001") {
                        var str = "<ul>";
                        for (var i = 0; i < res.data.length; i++) {
                            str +="<li>"+ res.data[i]+"</li>";
                        }
                        str += "</ul>";
                        layer.open({
                            title: '警告'
                            , content: str
                        }); 
                    }
                }, "json");
                layer.close(index);
                $(".leadinBox").css("display", "none");
            },
            btn2: function (index) {
                layer.close(index);
                $(".leadinBox").css("display", "none");
            }
            , cancel: function (index) {
                layer.close(index);
                
                $(".leadinBox").css("display", "none");
            }
            , end: function () {
                $(".chooseFile").css("display", "block");
                $(".chooseLoad").css("display", "none");
                $(".chooseTable").css("display", "none");
            }
        });
    });
    //搜索
    $(".search").off().on("click", function () {
        var uname = $("#uname").val();
        var month = $("#month").val();
        var table = layui.table;
        table.reload('payRollTab', {
            url: 'search'
            , loading: true
            , where: {
                uname: uname,
                month: month
            }
            , done: function (res) {
                if (res.data.length == 0) {
                    $("#noData").removeClass("layui-hide");
                    $(".layui-none").remove();
                    $(".layui-table-page").remove();
                } else {

                    $("#noData").addClass("layui-hide");
                }
            }
        });
    })
    
    
    //只能输入两位小数的数字
    $(document).off().on("keyup", "input.layui-table-edit", function () {
       
        $(this).val($(this).val().replace(/[^\d.]/g, "")) //清除"数字"和"."以外的字符
        $(this).val($(this).val().replace(/^\./g, ""))//验证第一个字符是数字
        $(this).val($(this).val().replace(/\.{2,}/g, "."))//只保留第一个, 清除多余的

        $(this).val($(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."))
        $(this).val($(this).val().replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3')) //只能输入两个小数
    })
    
});
layui.use('form', function () {
    var form = layui.form;
    var table = layui.table;
    form.on('select(month)', function (data) {
        table.reload('payRollTab', {
            url: 'search'
            , loading: true
            , where: {
                uname: $("#uname").val(),
                month: data.value
            }
            
            , done: function (res) {
                console.log(res)
                if (res.data.length == 0) {
                    $("#noData").removeClass("layui-hide");
                    $(".layui-none").remove();
                    $(".layui-table-page").remove();
                } else {

                    $("#noData").addClass("layui-hide");
                }
            }  

        });
    });  
});