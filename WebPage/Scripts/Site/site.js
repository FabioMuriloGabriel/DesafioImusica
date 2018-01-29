function returnDependentName(employeeId) {
    $.ajax({
        url: "/employee/returndependentname/" + employeeId,
        type: 'POST',
        cache: false,
        success: function (data) {
            $("#dependentNameId").text(data);
            $("#dependentNameId").val(data);
        }
    });
}

function returnName(employeeId) {
    $.ajax({
        url: "/employee/returnemployeename/" + employeeId,
        type: 'POST',
        cache: false,
        success: function (data) {
            $("#nameId").text(data);
            $("#nameId").val(data);
        }
    });
}

function returnEmployeeID(id) {
    $("#delId").text(id);
    $("#delId").val(id);
}

function findNameEmployee() {
    var employeeName = $("#findName").val();
    employeeName = escape(employeeName);

    $.ajax({
        url: "/employee/findexistnameemployee/" + employeeName,
        type: 'POST',
        cache: false,
        success: function (data) {
            var url = window.location.href;
            if (data != "") {
                var url2 = url.split("/");
                url = url2[0] + "/" + url2[1] + "/" + url2[2] + "/" + "employee" + "/" + "findnameemployee" + "/" + employeeName;
            }
                window.location.assign(url);
            
        }
    });
}

function DeleteEmployee() {
    var employeeId = $("#delId").val();
    var employeeName = $("#findName").val();

    $.ajax({
        url: "/employee/deleteemployee/" + employeeId,
        type: 'POST',
        cache: false,
        success: function (data) {
            var url = window.location.href;
            if (data == "") {
                if (url.split("/")[6] == null) {
                    url;
                }
                else {
                    var length = url.lastIndexOf("/");
                    url = url.substring(0, length);
                }
                window.location.assign(url);
            }
            else {
                var url2 = url.split("/");
                if (employeeName == "")
                    url = url2[0] + "/" + url2[1] + "/" + url2[2] + "/" + "employee" + "/" + "list" + "/" + data;
                else
                    url = url2[0] + "/" + url2[1] + "/" + url2[2] + "/" + "employee" + "/" + "findnameemployee" + "/" + employeeName;
                window.location.assign(url);
            }
        }
    });
}