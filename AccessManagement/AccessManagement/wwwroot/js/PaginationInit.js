//window.onload = function () {
//    new myPagination({
//        id: 'pagination',
//        curPage: 1, //初始页码
//        pageTotal: 50, //总页数
//        pageAmount: 20,  //每页多少条
//        dataTotal: 500, //总共多少条数据
//        pageSize: 10, //可选,分页个数
//        showPageTotalFlag: true, //是否显示数据统计
//        showSkipInputFlag: true, //是否支持跳转
//        getPage: function (page) {
//            //获取当前页数
//            console.log(page);
//        }
//    })

//}
function GetData(curPage, pageAmount = 20, url) {
    var filter = {
        CurPage = curPage,
        PageAmount = pageAmount
    };
    $.ajax({
        url: url,
        data: filter,
        dataType: "json",
        type: "POST",
        contentType: "application/json",
        error: function (data) {
            alert(data.message);
        },
        success: function (data) {
            if (data.status != 0) {
                alert(data.message);
                return;
            }
            var mesg = data.message;
            if (data.message == "ok") {
                mesg = "添加成功！";
            }
            alert(mesg);
            $("#addTrackModal").modal('hide');
            window.location.href = '/TMS/TrackInfo';
            //dtShipping.draw();
        }
    });
}
function PaginationInit(data) {
    new myPagination({
        id: 'pagination',
        curPage: data.curPage, //初始页码
        pageTotal: data.pageTotal, //总页数
        pageAmount: data.pageAmount,  //每页多少条
        dataTotal: data.dataTotal, //总共多少条数据
        pageSize: data.pageSize, //可选,分页个数
        showPageTotalFlag: true, //是否显示数据统计
        showSkipInputFlag: true, //是否支持跳转
        getPage: function (page) {
            //获取当前页数
            console.log(page);
        }
    })
}