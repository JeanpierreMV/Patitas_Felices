const   pagoButton = document.getElementById('pago-button'),
        pagoClose = document.getElementById('pago-close'),
        pagoContent = document.getElementById('pago-content')


/*===== pago  SHOW =====*/
/* Validate if constant exists */
    if(pagoButton) {
        pagoButton.addEventListener('click', () =>{ 
            pagoContent.classList.add('show-pago')
        })
    }

/*===== pago HIDDEN =====*/
/* Validate if constant exists */
    if(pagoClose) {
    pagoClose.addEventListener('click', () =>{
    pagoContent.classList.remove('show-pago')})
    }
