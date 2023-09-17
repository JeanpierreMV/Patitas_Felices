// login
const showHiddenPass = (logfpass, logfeye) =>{
    const input = document.getElementById(logfpass),
            iconEye = document.getElementById(logfeye);

            iconEye.addEventListener('click',()=>{
                if(input.type === 'password'){
                    input.type = 'text';

                    iconEye.classList.add('ri-eye-line')
                    iconEye.classList.remove('ri-eye-off-line')
                }else{
                    input.type = 'password'
                    iconEye.classList.remove('ri-eye-line')
                    iconEye.classList.add('ri-eye-off-line')
                }
            })
}

showHiddenPass('logf-pass','logf-eye');


//INICIO
/*=============== SHOW MENU ===============*/
const navMenu = document.getElementById('nav-menu'),
      navToggle = document.getElementById('nav-toggle'),
      navClose = document.getElementById('nav-close')

if(navToggle){
    navToggle.addEventListener('click',()=>{
        navMenu.classList.add('show-menu')
    })
}


if(navClose){
    navClose.addEventListener('click',()=>{
        navMenu.classList.remove('show-menu')
    })
}

/*=============== REMOVE MENU MOBILE ===============*/

const navLink = document.querySelectorAll('.nav__link');

const linkAction = () => {
  const navMenu = document.getElementById('nav-menu');
  navMenu.classList.remove('show-menu');
};

navLink.forEach(n => n.addEventListener('click', linkAction));

const scrollHeader = () => {
  const header = document.getElementById('header');
  this.scrolly >= 50? header.classList.add('scroll-header')
                    :header.classList.remove('scroll-header');
}

window.addEventListener('scroll', scrollHeader);
/*=============== CHANGE BACKGROUND HEADER ===============*/


/*=============== GSAP ANIMATION ===============*/