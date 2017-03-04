﻿using System;
using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;
using static LanguageExt.Prelude;

namespace LanguageExt.ClassInstances
{
    public struct MState<S, A> : MonadState<SState<S, A>, State<S, A>, S, A>, Monad<S, State<S, A>, A>
    {
        [Pure]
        public MB Bind<MONADB, MB, B>(State<S, A> ma, Func<A, MB> f) where MONADB : struct, Monad<S, MB, B> =>
            default(MONADB).Return(s1 =>
            {
                var (x, s2, bottom) = default(SState<S, A>).Eval(ma, s1);
                if (bottom) return (default(B), s1, true);
                return default(MONADB).Eval(f(x), s2);
            });

        [Pure]
        public State<S, A> Fail(object err) =>
            default(SState<S, A>).Bottom;

        [Pure]
        public State<S, A> Fail(Exception err = null) =>
            default(SState<S, A>).Bottom;

        [Pure]
        public State<S, A> Return(Func<S, (A, S, bool)> f) =>
            default(SState<S, A>).Lift(f);

        [Pure]
        public SSS Get<SStateS, SSS>()
            where SStateS : struct, StateMonadValue<SSS, S, S> =>
                default(SStateS).Lift(state => (state, state, false));

        [Pure]
        public SSU Put<SStateU, SSU>(S state)
            where SStateU : struct, StateMonadValue<SSU, S, Unit> =>
                default(SStateU).Lift(_ => (unit, state, false));

        [Pure]
        public State<S, A> Return(A x) =>
            State(s => (x, s, false));

        [Pure]
        public (A, S, bool) Eval(State<S, A> ma, S state) =>
            default(SState<S, A>).Eval(ma, state);

        [Pure]
        public State<S, A> State(Func<S, (A, S, bool)> f) =>
            default(SState<S, A>).Lift(state => f(state));
    }

    public struct MState<SStateA, SSA, S, A> : MonadState<SStateA, SSA, S, A>, Monad<S, SSA, A>
        where SStateA : struct, StateMonadValue<SSA, S, A>
    {
        [Pure]
        public MB Bind<MONADB, MB, B>(SSA ma, Func<A, MB> f) where MONADB : struct, Monad<S, MB, B> =>
            default(MONADB).Return(s1 =>
            {
                var (x, s2, bottom) = default(SStateA).Eval(ma, s1);
                if (bottom) return (default(B), s1, true);
                return default(MONADB).Eval(f(x), s2);
            });

        [Pure]
        public SSA Fail(object err) =>
            default(SStateA).Bottom;

        [Pure]
        public SSA Fail(Exception err = null) =>
            default(SStateA).Bottom;

        [Pure]
        public SSA Return(Func<S, (A, S, bool)> f) =>
            default(SStateA).Lift(f);

        [Pure]
        public SSS Get<SStateS, SSS>()
            where SStateS : struct, StateMonadValue<SSS, S, S> =>
                default(SStateS).Lift(state => (state, state, false));

        [Pure]
        public SSU Put<SStateU, SSU>(S state)
            where SStateU : struct, StateMonadValue<SSU, S, Unit> =>
                default(SStateU).Lift(_ => (unit, state, false));

        [Pure]
        public SSA Return(A x) =>
            State(s => (x, s, false));

        [Pure]
        public (A, S, bool) Eval(SSA ma, S state) =>
            default(SStateA).Eval(ma, state);

        [Pure]
        public SSA State(Func<S, (A, S, bool)> f) =>
            default(SStateA).Lift(state => f(state));
    }
}
