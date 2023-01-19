// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function addimg() {
    var division = document.createElement('divimg');
    division.innerHTML = Dynamicimg("");
    document.getElementById("divimg").appendChild(division)
}
function Dynamicimg(value) {
    return '<input type="file" class="form-control" asp-for="ProductIamges" />';

}

//var loginuser = function () {

//    debugger;
//    var data = $("#myform").serialize();

//    if (!$("#myform").valid())
//    {
//        return false;
//    }

//    $.ajax({
       
//        type: "post",
//        url: "/Login/Index",
//        data: data,
//        sucess: function (response) {
//            debugger;
//            if (response == "fail")
//                window.location.href = "/Login/Index";
//            else if (response == "fail") {
//                window.location.href = "/Admin/Index";
//            }
//            else {
//                window.location.href = "/Login/Index";
//            }
//        }
//        })
//}