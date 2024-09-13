# этап сборки (build stage)
FROM node:16.15.1-alpine as build-stage
WORKDIR /app
COPY ["nuxt-front/", "nuxt-front/"]
WORKDIR /app/nuxt-front
RUN npm install --location=global nuxt@2.15.8
RUN npm install
RUN npm run build
#RUN nuxt start

# этап production (production-stage)
FROM nginx:stable-alpine as production-stage
# COPY ["front/nginx", "/etc/nginx/conf.d"]
COPY --from=build-stage /app/nuxt-front/dist /usr/share/nginx/html
RUN apk add --no-cache --upgrade bash
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]