/*=============== SWIPER mascota ===============*/
let swiperMascotas = new Swiper(".home__swiper", {
    loop: true,
    spaceBetween:32,
    grabCursor: true,   
    effect: 'creative',
    creativeEffect:{
      prev:{
          tranlate:[-100, 0, -500],
          opacity:0,
      },
      next:{
        translate:[100, 0, -500],
        opacity: 0,
      },
    },
  
    pagination: {
      el: '.swiper-pagination',
      clickable: true,
    },
  });
/*=============== ===============*/
