function getAdverts(search, page) {
   
    $.ajax({
        type: "GET",
        url: "/Home/Search",
        data: {
            "str" : search,
            "page" : page
        },
        success: function (data) {
            $("#ajax").html(data);
        }
    });
}