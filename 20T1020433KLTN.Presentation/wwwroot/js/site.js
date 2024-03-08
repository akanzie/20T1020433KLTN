// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    // Lắng nghe sự kiện click vào nút toggle và thêm/xóa class "show" từ sidebar
    document.getElementById('sidebarToggle').addEventListener('click', function() {
        var sidebar = document.getElementById('sidebar');
    sidebar.classList.toggle('show');
    });
</script>