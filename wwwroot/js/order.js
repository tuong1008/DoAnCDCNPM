document
  .getElementById("myButton")
  .addEventListener("click", function (event) {
    event.preventDefault();
    if (!$("#myForm").valid()) return;
    let myForm = document.getElementById("myForm");
    let formData = new FormData(myForm);

    formData.append("loaiGD", document.getElementById("loaiGD").value);
    var object = {};
    formData.forEach(function (value, key) {
      object[key] = value;
    });
    console.log(object);

    $.ajax({
        type: "POST",
        url: "/Home/PostLenhDat",
        data: JSON.stringify(object),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(data){console.log(data);},
        error: function(errMsg) {
            console.log(errMsg.responseText);
        }
    });
  });
  $("#myForm").validate({
    onkeyup: function (element) {
        $(element).valid();
    },
    rules: {
        maCP: {
            required: true,
            maxlength: 7
        },
        soLuong: {
            required: true,
            digits: true,
            min: 1
        },
        giaDat: {
            required: true,
            number: true
        }
    },
    messages: {
        maCP: {
            required: "Bắt buộc nhập Mã chứng khoán",
            maxlength: "Hãy nhập tối đa 7 ký tự"
        },
        soLuong: {
            required: "Bắt buộc nhập Khối lượng",
            digit: "Chỉ được nhập số nguyên",
            min: "Hãy nhập Khối lượng lớn hơn 0"
        },
        giaDat: {
            required: "Bắt buộc nhập Giá",
            number: "Hãy nhập số thực"
        }
    }
});
