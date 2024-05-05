
<script>
    // Lắng nghe sự kiện click vào nút toggle và thêm/xóa class "show" từ sidebar
    document.getElementById('sidebarToggle').addEventListener('click', function() {
    var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
});


    const selectAllCheckbox = document.getElementById('selectAllClass1');
    const itemCheckboxes = document.querySelectorAll('#class1Students input[type="checkbox"]');

    // Xử lý sự kiện khi checkbox "Chọn tất cả" được thay đổi
    selectAllCheckbox.addEventListener('change', function() {
        // Lặp qua tất cả các checkbox phần tử và đặt trạng thái chọn dựa trên trạng thái của checkbox "Chọn tất cả"
        itemCheckboxes.forEach(function (checkbox) {
            checkbox.checked = selectAllCheckbox.checked;
        });
});

    // Xử lý sự kiện khi một checkbox phần tử khác được thay đổi
    itemCheckboxes.forEach(function(checkbox) {
        checkbox.addEventListener('change', function () {
            // Kiểm tra nếu tất cả các checkbox phần tử đã được chọn thì đánh dấu checkbox "Chọn tất cả" là đã được chọn
            selectAllCheckbox.checked = [...itemCheckboxes].every(function (item) {
                return item.checked;
            });
        });
});

    // Lắng nghe sự kiện khi tất cả các tài liệu HTML đã được tải
    document.addEventListener('DOMContentLoaded', function() {
    var itemCheckboxes = document.querySelectorAll('.checkboxItem');

    // Lắng nghe sự kiện nhập vào thanh tìm kiếm
    var searchInput = document.getElementById('searchInput');
    searchInput.addEventListener('input', function() {
        var searchText = searchInput.value.toLowerCase().trim();

    // Lặp qua tất cả các checkbox phần tử
    itemCheckboxes.forEach(function(checkbox) {
            var label = checkbox.nextElementSibling.textContent.toLowerCase().trim();
    // Nếu nội dung của label chứa chuỗi tìm kiếm thì hiển thị, ngược lại ẩn đi
    if (label.includes(searchText)) {
        checkbox.parentNode.style.display = 'block';
            } else {
        checkbox.parentNode.style.display = 'none';
            }
        });
    });

    var addStudentBtn = document.getElementById('addStudentBtn');
    var studentList = document.getElementById('studentList');

    addStudentBtn.addEventListener('click', function(event) {
        event.preventDefault(); // Ngăn chặn hành vi mặc định của nút submit (trang reload)

    var studentNameInput = document.getElementById('studentName');
    var studentName = studentNameInput.value.trim();

    if (studentName !== '') {
            var listItem = document.createElement('li');
    listItem.textContent = studentName;
    studentList.appendChild(listItem);

    // Xóa nội dung của ô nhập liệu sau khi thêm sinh viên thành công
    studentNameInput.value = '';
        }
    });
});
    window.addEventListener('click', function(event) {
    var modal = document.getElementById('myModal');
    if (event.target == modal) {
        modal.style.display = 'none';
    }
});

</script>