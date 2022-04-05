$(() => {
    //submit
    $(".btn-submit").click(e => {
        e.preventDefault();

        let model = getFormData();
        if (model != null) {
            ajax.post(
                formUrl,
                model,
                data => {
                    let send = confirm("send Email verifications?");
                    if (send) {
                        sendMails(data.listID);
                    }
                    window.open(editPage + "/" + data);
                },
                data => {
                    //TODO use other method to output model state
                    alert(data.responseJSON.ModelState["validationError"]);
                });
        }
        else {
            alert("you have blank fields that are required");
        }
    });
})