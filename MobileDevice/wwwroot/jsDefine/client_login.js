
function checkrong_login() {
    let taikhoan = $('#login_taikhoan').val();
    let matkhau = $('#login_matkhau').val();

    if (taikhoan == '') {
        GrowlNotification.notify({
            description: 'Tài khoản không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
    if (matkhau == '') {
        GrowlNotification.notify({
            description: 'Mật khẩu không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}
function val_login_dangnhap() {
    let taikhoan = $('#login_taikhoan').val();
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (regExp.test(taikhoan)) {
        GrowlNotification.notify({
            description: 'Tài khoản không được chứa ký tự đặc biệt!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}