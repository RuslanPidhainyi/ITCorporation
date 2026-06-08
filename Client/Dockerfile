# Stage 1: Build Angular
FROM node:20-alpine AS build
WORKDIR /app

COPY Client/package*.json .
RUN npm ci

COPY Client/ .
RUN npm run build

# Stage 2: nginx
FROM nginx:alpine
COPY --from=build /app/dist/client/browser /usr/share/nginx/html
COPY Client/nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
