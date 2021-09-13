$(document).ready(function () {
    $('#IdStudent').on("change", function () {
        var IdStudent = $('#IdStudent').val();
        $.ajax({
            url: '/Grades/GetSubjectsOfStudent',
            type: 'GET',
            data: {
                IdStudent: IdStudent
            },
            success: manageSubjects
        });
    })

    function manageSubjects(result) {
        var options = '';
        $('#IdSubject').empty();
        options += '<option> Select Subject </option>';
        for (let i = 0; i < result.length; i++) {
            options += '<option value="' + result[i].value + '">' + result[i].text + '</option>';
        }
        $('#IdSubject').append(options);
    }
});