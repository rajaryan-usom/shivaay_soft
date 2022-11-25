<script>
    $(document).ready(function () {
        $("#datatable").DataTable({
            "processing": true,
            "serverSide": true, // enabling server side
            "filter": true, //set true for enabling searching
            "ajax": {
                "url": "/product/GetProductList",// ajax url to load content
                "type": "POST", // type of method to call
                "datatype": "json" // return datatype
            },
            "columns": [
                { "data": "title", "name": "title", "autoWidth": true }, // columns name and related settings
                { "data": "excerpt", "name": "excerpt", "autoWidth": true }, // columns name and related settings
                { "data": "description", "name": "description", "autoWidth": true }, // columns name and related settings
                { "data": "filePath", "name": "filePath", "autoWidth": true }, // columns name and related settings
            ]
        });
        });
</script>

