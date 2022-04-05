$(() => {
    let hideEl = function () {
        $("#btn-load-login").hide();
        $("#btn-load-register").hide();
        $("#h2-text").hide();
    };
    $("#btn-load-login").click(e => {
        $('#for-partial').load(loginUrl);
        hideEl();
    });
    $("#btn-load-register").click(e => {
        $('#for-partial').load(registerUrl);
        hideEl();
    });

    //Delete list
    $(".delete").click(e => {
        //TODO use other method to output confirm
        if (confirm("Are you sure you want to delete this list?")) {
            let data = e.target.id.replace("delete-", "");
            console.log(data)
            ajax.delete(
                deleteUrl + "/" + data,
                null,
                data => {
                    let elID = e.target.id;
                    $("#" + elID).parent().parent().hide("slow");
                },
                data => {
                    //TODO use other method to output model state
                    alert(data.responseJSON.ModelState["deleteError"]);
                }
            );
        }
    })
})