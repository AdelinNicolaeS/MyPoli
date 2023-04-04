$(document).ready(function () {
    $('#IdSubject').on("change", function () {
        var IdSubject = $('#IdSubject').val();
        $.ajax({
            url: '/Grades/GetStudentsOfSubjectAndGroup',
            type: 'GET',
            data: {
                IdSubject: IdSubject,
            },
            success: manageStudents
        });
    })

    function manageStudents(result) {
        var options = '';
        $('#IdStudent').empty();
        options += '<option> Select Student </option>';
        for (let i = 0; i < result.length; i++) {
            options += '<option value="' + result[i].value + '">' + result[i].text + '</option>';
        }
        $('#IdStudent').append(options);
    }
});