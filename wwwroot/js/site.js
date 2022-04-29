// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(()=>{
    let connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build()

    connection.start()

    connection.on("refreshLenhDats",function(){
        loadData();
    })

    loadData();

    function loadData(){
        var tr = ''

        $.ajax({
            url: '/Home/GetLenhDats',
            method: 'GET',
            success: (result)=>{
                $.each(result,(k,v)=>{
                    let tempLoaiGD = "";
                    if (v.loaiGD === "M"){
                        tempLoaiGD = "Mua";
                    } else {
                        tempLoaiGD = "Bán";
                    }
                    tr = tr + `<tr>
                        <td>${v.id}</td>
                        <td>${v.maCP}</td>
                        <td>${v.ngayDat}</td>
                        <td>${tempLoaiGD}</td>
                        <td>${v.loaiLenh}</td>
                        <td>${v.soLuong}</td>
                        <td>${v.giaDat}</td>
                        <td>${v.trangThaiLenh}</td>
                    </tr>`
                })

                $("#tableBody").html(tr)
            },
            error: (error)=>{
                console.log(error)
            }
        })
    }
});
