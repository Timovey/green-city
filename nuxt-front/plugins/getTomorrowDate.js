export default ({app}, inject) => {
    inject('getTomorrowDate', function(){
        return app.$correctDate(new Date(Date.now() + 24 * 60 * 60 * 1000))
    })
}
