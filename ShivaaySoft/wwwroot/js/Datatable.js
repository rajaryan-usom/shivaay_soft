$(document).ready(function () {
    GetProducts();

    //$("#datatable").DataTable({
    //    "processing": true,
    //    "serverSide": true, // enabling server side
    //    "filter": true, //set true for enabling searching
    //    "ajax": {
    //        "url": "/product/GetProductList",// ajax url to load content
    //        "type": "POST", // type of method to call
    //        "datatype": "json" // return datatype
    //    },
    //    "columns": [
    //        { "data": "title", "name": "title", "autoWidth": true }, // columns name and related settings
    //        { "data": "excerpt", "name": "excerpt", "autoWidth": true }, // columns name and related settings
    //        { "data": "description", "name": "description", "autoWidth": true }, // columns name and related settings
    //        { "data": "filePath", "name": "filePath", "autoWidth": true }, // columns name and related settings
    //    ]
    //});
});


function GetProducts() {
    $.ajax({
        url: '/product/GetProduct',
        type: 'Get', // type of method to call
        datatype: 'Json', // return datatype
        success: OnSuccess


    })
};

//after success all the data will be stored on response 
function OnSuccess(response) {
    $('#datatable').DataTable({
        bProcessing: true,
        BLengthChange: true,
        lengthMenu: [[5, 10, 20, -1], [5, 10, 20, "All"]],
        bFilter: true,
        bPaginate: true,
        data: response,
        columns: [
            { data: 'Id', render: function (data, type, row, meta) { return row.id } },// columns name and related settings
            { data: 'Title', render: function (data, type, row, meta) { return row.title } },
            { data: 'Excerpt', render: function (data, type, row, meta) { return row.excerpt } },
            { data: 'Description', render: function (data, type, row, meta) { return row.description } },
        ]
    });

}
