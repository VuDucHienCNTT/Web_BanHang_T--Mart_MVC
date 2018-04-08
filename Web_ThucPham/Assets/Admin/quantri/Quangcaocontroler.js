var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/QuangCao/ThayDoiTrangThai",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.trangthai == true) {
                        btn.text("Đã duyệt");
                    }
                    else {
                        btn.text("Chờ duyệt");
                    }
                }
            });
        });
    }
}
user.init();