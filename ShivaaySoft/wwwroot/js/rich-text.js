ClassicEditor

    .create(document.querySelector('.rich-text-area'), {

        toolbar: ['heading', '|', 'bold', 'italic', 'link', 'bulletedList', 'numberedList', 'blockQuote'],
    })

    .catch(error => {

        console.log(error);

    });