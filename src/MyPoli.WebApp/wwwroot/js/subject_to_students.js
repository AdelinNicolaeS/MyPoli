$(document).ready(function () {
    $('#IdSubject, #IdGroup').on("change", function () {
        var IdSubject = $('#IdSubject').val();
        var IdGroup = $('#IdGroup').val();
        $.ajax({
            url: '/Grades/GetStudentsOfSubjectAndGroup',
            type: 'GET',
            data: {
                IdSubject: IdSubject,
                IdGroup: IdGroup
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