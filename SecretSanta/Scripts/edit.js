let submitEdit;
let startEdit;
let sendMail;

function addReadonly() {
    $("input").attr("readonly", "readonly");
}
function removeReadonly() {
    //
    $("input").removeAttr("readonly");
    ownerData.eq(0).attr("readonly", "readonly");
}

function startEditing() {
    $(".remove").show();
    $(".no-remove").show();
    $(".add-user").show();
    submitEdit.show();
    $(".hidable").hide();
    $(".mail-warning").hide();
    startEdit.hide();
    sendMail.hide();

    removeReadonly();
}
function save() {
    $(".no-remove").hide();
    $(".remove").hide();
    $(".add-user").hide();
    submitEdit.hide();
    $(".mail-warning").show();
    startEdit.show();
    sendMail.show();

    addReadonly();
}

$(() => {
    submitEdit = $(".btn-submit-edit");
    startEdit = $(".btn-edit");
    sendMail = $(".btn-send-mails");
    userInputM = $("tr.user-data").last()

    save();
    $(".mail-warning").hide();

    //start editing
    startEdit.click(e => {
        e.preventDefault();

        startEditing();
    })

    //submit editing
    submitEdit.click(e => {
        e.preventDefault();

        let model = getFormData();
        extendFormData(model);

        if (model != null) {
            ajax.put(
                formUrl,
                model,
                data => {
                    save();
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

    //send mails
    sendMail.click(e => {
        e.preventDefault();

        sendMails($("metaData.list-data > input").eq(1).val());
    });
})