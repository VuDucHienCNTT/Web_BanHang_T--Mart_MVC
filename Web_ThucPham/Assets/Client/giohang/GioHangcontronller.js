var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        $('#btnMuaTiep').off('click').on('click', function () {
            window.location.href = "/";
        });

        $('#btnThanhToan').off('click').on('click', function () {
            window.location.href = "/GioHang/ThanhToan";
        });

        $('#btnCapNhat').off('click').on('click', function () {
            var dsSanPham = $('.txtsoluong');
            var cartList = []
            $.each(dsSanPham, function (i, item) {
                cartList.push({
                    Soluong: $(item).val(),
                    SanPham: {
                        Id: $(item).data('id')
                    }
                })
            });

            $.ajax({
                url: 'GioHang/CapNhat',
                data: { cartModel: JSON.stringify(cartList) },
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.trangthai == true) {
                        window.location.href = "/GioHang";
                    }
                }
            })
        });

        $('#btnXoaGioHang').off('click').on('click', function () {
            $.ajax({
                url: 'GioHang/XoaGioHang',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.trangthai == true) {
                        window.location.href = "/GioHang";
                    }
                }
            })
        });

        $('.btnxoa1SP').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                data: { id: $(this).data('id') },
                url: 'GioHang/Xoa1SP',
                dataType: 'json',
                type: 'POST',
                success: function (res) {
                    if (res.trangthai == true) {
                        window.location.href = "/GioHang";
                    }
                }
            })
        });
    }
}
cart.init();

function IsEmail(email) {
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}