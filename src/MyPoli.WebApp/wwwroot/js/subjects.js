window.addEventListener("load", () => {
    $.ajax({
        type: 'GET',
        url: '/Subjects/GetSubjects',
        data: {

        },
        success: (result) => {
            $("#subjectSelect").select2({
                // data: result,
                multiple: true
            })
        }
    });
    $("#submit").click((e) => {
        $("subjectSelect *").removeAttr("selected");
        $(":selected").attr("selected", "selected");
    });
})