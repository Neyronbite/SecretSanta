$(() => {
    
})

function getRandomInt(min, max) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

let ajax = {
    get: function (url, data, succes, error) {
        $.ajax({
            url: url,
            type: "GET",
            data: data,
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: data => {
                if (error != null) {
                    error(_data);
                }
            }
        })
    },
    post: function (url, data, succes, error) {
        $.ajax({
            url: url,
            type: "POST",
            data: data,
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: _data => {
                if (error != null) {
                    error(_data);
                }
            }
        })
    },
    put: function (url, data, succes, error) {
        $.ajax({
            url: url,
            type: "PUT",
            data: data,
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: _data => {
                if (error != null) {
                    error(_data);
                }
            }
        })
    },
    delete: function (url, data, succes, error) {
        $.ajax({
            url: url,
            type: "DELETE",
            data: data,
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: _data => {
                if (error != null) {
                    error(_data);
                }
            }
        })
    },
    postForm: function (url, formSelector, succes, error) {
        $.ajax({
            url: url,
            type: 'POST',
            data: $(formSelector).serialize(),
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: _data => {
                if (error != null) {
                    error(_data);
                }
            }
        });
    },
    postFormWithFile: function (url, formSelector, succes, error) {
        var formData = new FormData($(formSelector)[0]);

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            success: _data => {
                if (succes != null) {
                    succes(_data);
                }
            },
            error: _data => {
                if (error != null) {
                    error(_data);
                }
            },
            cache: false,
            contentType: false,
            processData: false
        });
    }
}