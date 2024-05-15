function showToast(message) {
    var toast = $('<div class="toast-container position-fixed bottom-0 end-0 p-3">\
                    <div class="toast" role="alert" aria-live="assertive" aria-atomic="true">\
                        <div class="toast-body">\
                            ' + message + '\
                        </div>\
                    </div>\
                </div>');

    // Thêm toast vào body
    $('body').append(toast);

    // Hiển thị toast
    var bsToast = new bootstrap.Toast(toast.find('.toast')[0]);
    bsToast.show();
}
function openModalWithConfirmation(header, message, buttonName, confirmCallback) {
    // Tạo modal
    var modal = $('<div class="modal fade" tabindex="-1" role="dialog">\
                    <div class="modal-dialog" role="document">\
                        <div class="modal-content">\
                            <div class="modal-header">\
                                <h5 class="modal-title">' + header + '</h5>\
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>\
                            </div>\
                            <div class="modal-body">' + message + '</div>\
                            <div class="modal-footer">\
                                <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Hủy</button>\
                                <button type="button" class="btn btn-primary btn-confirm"m>' + buttonName + '</button>\
                            </div>\
                        </div>\
                    </div>\
                </div>');

    // Thêm modal vào body
    $('body').append(modal);

    // Mở modal
    modal.modal('show');

    // Xác nhận khi click vào button "Xác nhận"
    modal.find('.btn-confirm').on('click', function () {
        if (typeof confirmCallback === 'function') {
            confirmCallback();
        }
        // Đóng modal sau khi xác nhận
        modal.modal('hide');
    });

    // Xóa modal khỏi DOM khi đóng
    modal.on('hidden.bs.modal', function () {
        modal.remove();
    });
}
function handleAjaxError(xhr) {
    var errorMessage = xhr.responseText;
    alert(errorMessage);
}
function checkFileSize(input) {
    var files = input.files; // Mảng các file được chọn

    for (var i = 0; i < files.length; i++) {
        var fileSize = files[i].size; // Kích thước của file trong byte
        var maxSizeInBytes = 25 * 1024 * 1024; // Giới hạn kích thước file (25MB)

        if (fileSize > maxSizeInBytes) {
            showToast("Kích thước file " + files[i].name + " vượt quá giới hạn cho phép (25MB). Vui lòng chọn file khác.");
            // Xoá file đã chọn (tùy chọn)
            input.value = "";
            return false; // Kích thước file vượt quá giới hạn
        }
    }

    return true; // Tất cả các file đều hợp lệ
}





