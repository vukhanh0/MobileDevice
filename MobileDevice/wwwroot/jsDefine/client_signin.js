

function checkrong() {
    let tendangnhap = $('#signin_dangnhap').val();
    let email = $('#signin_email').val();
    let hoten = $('#signin_hoten').val();
    let matkhau = $('#signin_matkhau').val();
    let matkhau2 = $('#signin_matkhau2').val();
    if (tendangnhap == '') {
        //$('#error_dangnhap').show();
        //$('#error_dangnhap').text("Tên đăng nhập không được để trống !");
        GrowlNotification.notify({
            description: 'Tài khoản không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
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
    if (hoten == '') {
        //$('#error_dangnhap').show();
        //$('#error_dangnhap').text("Tên đăng nhập không được để trống !");
        GrowlNotification.notify({
            description: 'Họ tên không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
    if (matkhau == '') {
        //$('#error_dangnhap').show();
        //$('#error_dangnhap').text("Tên đăng nhập không được để trống !");
        GrowlNotification.notify({
            description: 'Mật khẩu không được để trống !',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
    if (matkhau2 == '') {
        //$('#error_dangnhap').show();
        //$('#error_dangnhap').text("Tên đăng nhập không được để trống !");
        GrowlNotification.notify({
            description: 'Bạn chưa nhập lại mật khẩu!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}
function val_dangnhap() {
    let tendangnhap = $('#signin_dangnhap').val();
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (regExp.test(tendangnhap)) {
        GrowlNotification.notify({
            description: 'Tài khoản không được chứa ký tự đặc biệt!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
    if (tendangnhap.length > 50) {
        GrowlNotification.notify({
            description: 'Tài khoản không được quá 50 ký tự!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}
function val_email() {
    let email = $('#signin_email').val();
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
function val_hoten() {
    let hoten = $('#signin_hoten').val();
    var regExp = /[`!@#$%^&*()+=\[\]{};':"\\|,.<>\/?~]/;
    if (regExp.test(hoten)) {
        GrowlNotification.notify({
            description: 'Họ tên không được chứa ký tự đặc biệt!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
    else {
        let str = "";
        let hoten = hoten.replace(/\s+/g, ' ').trim();
        let check = hoten.split(' ');
        for (i = 0; i < check.length; i++) {
            let chuoi = check[i].split('');
            let chuyenhoa = chuoi[0].toUpperCase();
            check[i] = check[i].replace(chuoi[0], chuyenhoa);
            str += check[i] + " ";
        }
        let newstr = str.trim();
        $('#signin_hoten').val(newstr);
    }

}
function val_pass2() {
    let pass = $('#signin_matkhau').val();
    let pass2 = $('#signin_matkhau2').val();
    if (pass2 != pass) {
        GrowlNotification.notify({
            description: 'Mật khẩu nhập lại không chính xác!',
            type: 'error',
            position: 'top-right',
            closeTimeout: 3000
        });
    }
}
