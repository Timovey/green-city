import { getItem, setItem } from "~/helpers/persistanceStorage"
import {mapActions} from 'vuex'
export default async function(ctx) {
    let user = getItem('user')
    if (!user) {
        console.log(user, 'user')
        return await ctx.redirect('/login')
    }
    // console.log(ctx);
   
    await ctx.store.dispatch('auth/setUser', user)
}