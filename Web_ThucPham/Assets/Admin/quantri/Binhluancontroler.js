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
                url: "/Admin/BinhLuan/ThayDoiTrangThai",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.trangthai == true) {
                        btn.text("Hiển thị");
                    }
                    else {
                        btn.text("Không hiển thị");
                    }
                }
            });
        });
    }
}
user.init();