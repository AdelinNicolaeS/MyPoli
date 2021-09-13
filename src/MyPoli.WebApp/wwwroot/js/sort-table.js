$(document).ready(function () {
    $('#users-sort-table').DataTable({
        "paging": false // false to disable pagination (or any other option)
    });
    $('.dataTables_length').addClass('bs-select');
});