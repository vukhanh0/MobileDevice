

function checkrong_forgotpass() {
    let email = $('#forgot_email').val();
    if (email == '') {
        //$('#error_dangnhap').show();
        //$('#error_dangnhap').text("Tên đăng nhập không được để trống !");
        GrowlNotification.notify({
            description: 'Email không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}

function val_forgot_email() {
    let email = $('#forgot_email').val();
    var reg = /^[a-z][a-z0-9_\.]{5,32}@[a-z0-9]{2,}(\.[a-z0-9]{2,4}){1,2}$/;
    if (!reg.test(email)) {
        GrowlNotification.notify({
            description: 'Email không đúng định dạng!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}