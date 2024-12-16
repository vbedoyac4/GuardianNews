import { createRouter, createWebHistory } from "vue-router";
import NewsSearch from "../components/NewsSearch.vue";

const routes = [
  {
    path: "/",
    name: "Home",
    component: NewsSearch,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
