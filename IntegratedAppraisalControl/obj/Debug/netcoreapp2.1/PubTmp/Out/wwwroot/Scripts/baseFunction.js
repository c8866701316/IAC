function createDatatable(tableName, columns, pageSize, columnDefs, data, showPaging = false, ShowCommonSearch = false, ShowCoulmnFilters = false) {
    tableName = '#' + tableName;
    //if (ShowCoulmnFilters) {
    //    $(tableName + ' .filters td').each(function () {
    //        var title = $(tableName + ' thead th').eq($(this).index()).text().trim();
    //        if (title != "") {
    //            $(this).html('<input type="text" class="cust-input-sm" placeholder="Search ' + title.trim() + '" />');
    //        }
    //    });
    //}
    //else {
    //    $(tableName + ' .filters').remove();
    //}

    $(tableName + ' .filters').remove();

    var table = $(tableName).DataTable({
        data: data,
        columns: columns,
        destroy: true,
        "searching": ShowCommonSearch,
        "aLengthMenu": [
            [5, 10, 15, 20, -1],
            [5, 10, 15, 20, "All"]
        ],
        "bInfo": false,
        "language": {
            "emptyTable": "No data available in table"
        },
        "iDisplayLength": pageSize,
        "columnDefs": columnDefs,
        "bPaginate": showPaging
    });
    //.columns().eq(0).each(function (colIdx) {
    //$('input', $(tableName + ' .filters td')[colIdx]).on('keyup change', function () {
    //        table
    //            .column(colIdx)
    //            .search(this.value)
    //            .draw();
    //    });
    //});
    $('.dataTable').removeAttr('style');
}

function InitializeDatatable(tableName, url, columns, colDefs, pageSize, showPaging = false, ShowCommonSearch = false, ShowCoulmnFilters = false) {
    $.ajax({
        url: url,
        type: 'GET',
        dataType: "json",
        success: function (response) {
            createDatatable(tableName, columns, pageSize, colDefs, response.data, showPaging, ShowCommonSearch, ShowCoulmnFilters);
        },
        error: function (req, status, error) {
            alert('There is some issue while loading table data.');
        }
    });
}

/*Image upload preview*/
function setPreview(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            var prev = $(input).siblings('img')[0];
            $(prev).attr('src', e.target.result);
            $(prev).hide();
            $(prev).fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

$(".fileup").change(function () {
    setPreview(this);
});

function fillDDLFromOther(ddlName, url) {
    var ddl = $('#' + ddlName);
    ddl.empty();
    $.getJSON(url, function (response) {
        $.each(response.data, function (index, item) {
            ddl.append($('<option></option>').text(item.Text).val(item.Value));
        });
    });
}

function fnFormvalidate (formName) {
    var form1 = $('#' + formName);
    var error1 = $('.alert-danger', form1);
    var success1 = $('.alert-success', form1);

    form1.validate({
        doNotHideMessage: true,
        errorElement: 'span', //default input error message container
        errorClass: 'help-block', // default input error message class
        // errorClass: 'validate-inline',
        focusInvalid: false, // do not focus the last invalid input
        ignore: "",

        rules: {
            //account dateFuture
        },

        messages: {

        },

        invalidHandler: function (event, validator) { //display error alert on form submit
            for (var i = 0; i < validator.errorList.length; i++) {
                console.log(validator.errorList[i]);
            }

            //validator.errorMap is an object mapping input names -> error messages
            for (var i in validator.errorMap) {
                console.log(i, ":", validator.errorMap[i]);
            }
            success1.hide();
            error1.show();
            //App.scrollTo(error1, -200);
        },

        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        unhighlight: function (element) { // revert the change done by hightlight
            $(element)
                .closest('.form-group').removeClass('has-error'); // set error class to the control group
        },

        success: function (label) {
            label
                .closest('.form-group').removeClass('has-error'); // set success class to the control group
        },

        submitHandler: function (form1) {
            success1.show();
            error1.hide();
        }

    });
}


