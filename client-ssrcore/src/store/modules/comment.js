import SSRCore from '../../service/SSRCore'
const API_URL = '/api/v1/Comments'

export const comment = {
  namespaced: true,
  state: {
    _commentList: []
  },
  getters: {},
  mutations: {
    _setCommentList (state, _commentList) {
      state._commentList = _commentList
    },
    _addCommentMutations (state, comment) {
      state._commentList.push(comment)
    },
    async _updateCommentMutations (state, comment) {
      const commentObj = await state._commentList.find(x => x.Id === comment.Id)
      commentObj.title = comment.title
      commentObj.content = comment.content
      commentObj.categoryCode = comment.categoryCode
    },
    async _deleteCommentMutations (state, Id) {
      await state._commentList.pop(x => x.Id === Id)
    }
  },
  actions: {
    _getCommentList (context, obj) {
      return SSRCore.get(API_URL, obj).then(
        response => {
          context.commit('_setCommentList', response.data.data)
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _addComment (context, obj) {
      return SSRCore.post(API_URL, obj).then(
        response => {
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _updateComment (context, obj) {
      return SSRCore.put(API_URL + obj.Id, obj).then(
        response => {
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    },
    _deleteComment (context, obj) {
      return SSRCore.delete(API_URL + obj.Id).then(
        response => {
          return response.data
        },
        error => {
          return Promise.reject(error)
        }
      )
    }
  }
}
