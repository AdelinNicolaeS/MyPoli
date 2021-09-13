window.addEventListener("load", () => {
    $.ajax({
        type: 'GET',
        url: '/Groups/GetGroups',
        data: {

        },
        success: (result) => {
            $("#groupSelect").select2({
                // data: result,
                multiple: true
            })
        }
    });
    $("#submit").click((e) => {
        $("groupSelect *").removeAttr("selected");
        $(":selected").attr("selected", "selected");
    });
})