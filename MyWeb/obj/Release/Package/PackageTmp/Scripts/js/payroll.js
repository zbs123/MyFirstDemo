layui.use('table', function () {
    var table = layui.table;
    own.loading.show();
    own.ajax.post("GetPayRollData", {}, function (res) {
        if (res.code == "0") {
            //表格
            table.render({
                elem: '#payRollTab'
                // , url: 'demo.json'
                , data: res.data

                , limit: 100
                , cols: [[
                    { field: 'Month', width: 140, title: ' 时间' }
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
        }
    }, "json")
    
});