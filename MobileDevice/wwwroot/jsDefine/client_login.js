
function checkrong_login() {
    let taikhoan = $('#login_taikhoan').val();
    let matkhau = $('#login_matkhau').val();
    let success = $('#Success').val();
    let error1 = $('#Error1').val();
    let error2 = $('#Error2').val();
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

    //if (success != '') {
    //    GrowlNotification.notify({
    //        description: 'Đăng nhập thành công!',
    //        type: 'success',
    //        position: 'top-right',
    //        closeTimeout: 3000
    //    });
    //}
    //else if (error1 != '') {
    //    GrowlNotification.notify({
    //        description: 'Tài khoản không tồn tại !',
    //        type: 'error',
    //        position: 'top-right',
    //        closeTimeout: 3000
    //    });
    //}
    //else if (error2 != '') {
    //    GrowlNotification.notify({
    //        description: 'Tên đăng nhập hoặc mật khẩu không đúng !',
    //        type: 'error',
    //        position: 'top-right',
    //        closeTimeout: 3000
    //    });
    //}
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